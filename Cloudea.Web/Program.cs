using Cloudea.Domain.Common;
using Cloudea.Domain.Common.Repositories;
using Cloudea.Infrastructure.BackgroundJobs;
using Cloudea.Infrastructure.BackgroundJobs.ForumPostRecommendSystem;
using Cloudea.Persistence;
using Cloudea.Web.Infrastructure;
using Cloudea.Web.Middlewares;
using Cloudea.Web.OptionsSetup;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Quartz;
using Serilog;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Cloudea.RealTime.Hubs;
using Cloudea.RealTime;

namespace Cloudea.Web
{
    public class Program
    {
        /// <summary>
        /// Main Program Entry
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            // Create WebApplicationBuilder instance.
            var builder = WebApplication.CreateBuilder(args);

            // Print icon in console.
            OutputFile.OutputTxt(builder.Configuration);

            #region Add services to the container. Use Configuration to config the services.
            // Appsettings.json to Configurations
            builder.Services.ConfigureOptions<DatabaseOptionsSetup>();
            builder.Services.ConfigureOptions<JwtOptionsSetup>();
            builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();
            builder.Services.ConfigureOptions<MailOptionsSetup>();

            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();

            // Controller / Controller json options.
            builder.Services
                .AddControllers()
                .AddJsonOptions(options => {
                    options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            // Swagger API document options.
            builder.Services.AddSwaggerGen(options => {
                // Swagger Doc
                options.SwaggerDoc("v1", new OpenApiInfo {
                    Version = "v1",
                    Title = "Cloudea API",
                    Description = "Building...",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact {
                        Name = "Example Contact",
                        Url = new Uri("https://example.com/contact")
                    },
                    License = new OpenApiLicense {
                        Name = "Example License",
                        Url = new Uri("https://example.com/license")
                    }
                });

                // Using System.Reflection to create API document automaticly.
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

                // Swagger authentication function options.
                var openApiSecurity = new OpenApiSecurityScheme {
                    Name = "Authorization",
                    Description = "JWT认证授权，使用直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Reference = new OpenApiReference {
                        Type = ReferenceType.SecurityScheme,
                        Id = JwtBearerDefaults.AuthenticationScheme
                    }
                };
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, openApiSecurity);
                options.AddSecurityRequirement(new OpenApiSecurityRequirement{
                     { openApiSecurity, Array.Empty<string>() }
                });
            });

            // MVC service
            builder.Services.AddMvc();

            // WebSocket service - SignalR
            builder.Services.AddSignalR();

            // API authentication service
            builder.Services
                .AddAuthentication(x => {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer((options) => {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters {

                        // 颁发者
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],

                        // 颁发密钥
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"])),

                        // 受颁发人
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["Jwt:Audience"],

                        // 密钥生存周期
                        ValidateLifetime = true
                    };
                });
            builder.Services.AddAuthorization();

            // Http context accessor service
            builder.Services.AddHttpContextAccessor();

            // Http client
            builder.Services.AddHttpClient();

            // MemoryCache service
            builder.Services.AddMemoryCache();

            // MediatR
            builder.Services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssemblies(AssemblyLoader.GetAllAssemblies());
            });

            // CORS
            builder.Services.AddCors(opt => {
                opt.AddDefaultPolicy(b => {
                    b.WithOrigins(
                        "http://localhost:3000",
                        "https://www.cloudea.work")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                });
            });

            // Logservice - Serilog
            builder.Host.UseSerilog((context, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration));

            // Persistence - Efcore
            builder.Services.AddDbContext<ApplicationDbContext>(
                (IServiceProvider serviceProvider, DbContextOptionsBuilder dbContextOptionsBuilder) => {
                    var optionObj = serviceProvider.GetService<IOptions<DatabaseOptions>>() ?? throw new NotImplementedException();
                    var databaseOptions = optionObj.Value;

                    dbContextOptionsBuilder.UseMySql(databaseOptions.ConnectionString, ServerVersion.Parse("8.0.26"), mysqlAction => {
                        mysqlAction.EnableRetryOnFailure(databaseOptions.MaxRetryCount);
                        mysqlAction.CommandTimeout(databaseOptions.CommandTimeout);
                    });

                    dbContextOptionsBuilder.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);

                    dbContextOptionsBuilder.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);
                });

            // Scheduling framework - Quartz
            builder.Services.AddQuartz(configure => {
                // ProcessOutboxMessagesJob
                var processOutboxMessagesJobKey = new JobKey(nameof(ProcessOutboxMessagesJob));
                configure
                    .AddJob<ProcessOutboxMessagesJob>(processOutboxMessagesJobKey)
                    .AddTrigger(
                        trigger =>
                            trigger.ForJob(processOutboxMessagesJobKey)
                                .WithSimpleSchedule(
                                    schedule =>
                                        schedule.WithIntervalInSeconds(10)
                                            .RepeatForever()));

#if !DEBUG
var calculateUserPostInterestJobKey = new JobKey(nameof(CalculateUserPostInterestJob));
                configure
                    .AddJob<CalculateUserPostInterestJob>(calculateUserPostInterestJobKey)
                    .AddTrigger(
                        trigger =>
                            trigger.ForJob(calculateUserPostInterestJobKey)
                                .WithSimpleSchedule(
                                    schedule =>
                                        schedule.WithIntervalInSeconds(60)
                                            .RepeatForever()));

                var calculatePostSimilarityJobKey = new JobKey(nameof(CalculatePostSimilarityJob));
                configure
                    .AddJob<CalculatePostSimilarityJob>(calculatePostSimilarityJobKey)
                    .AddTrigger(
                        trigger =>
                            trigger.ForJob(calculatePostSimilarityJobKey)
                                .WithSimpleSchedule(
                                    schedule =>
                                        schedule.WithIntervalInSeconds(60)
                                            .RepeatForever()));

                var calculateUserSimilarityJobKey = new JobKey(nameof(CalculateUserSimilarityJob));
                configure
                    .AddJob<CalculateUserSimilarityJob>(calculateUserSimilarityJobKey)
                    .AddTrigger(
                        trigger =>
                            trigger.ForJob(calculateUserSimilarityJobKey)
                                .WithSimpleSchedule(
                                    schedule =>
                                        schedule.WithIntervalInSeconds(60)
                                            .RepeatForever()));
#endif
            });
            builder.Services.AddQuartzHostedService();

            // Auto inject services in other class library projects.
            builder.Services.RunModuleInitializers();
            #endregion

            //Build Webapplication.
            var app = builder.Build();

            #region Configure the HTTP request pipeline.
            app.Logger.LogInformation("Start running...");

            // Exception handler
            app.UseExceptionHandler();
#if !DEBUG
            // Swagger lock
            app.UseMiddleware<SwaggerAuthMiddleware>();
#endif
            // Api document
            app.UseSwagger();
            app.UseSwaggerUI();

            // Redirect to https
            app.UseHttpsRedirection();

            // Allow X-HTTP-Method-Override property on http request header.
            app.UseHttpMethodOverride();

            // Router
            app.UseRouting();
            // Cors
            app.UseCors();

            // Map Websocket Endpoint
            app.MapCloudeaHubs();

            // Map Controllers
            app.MapControllers();

            // Log API request
            app.UseSerilogRequestLogging();

            // Authentication and authorization / Generate login id.
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<UserLoginGuidAuthenticationMiddleware>();

            // Static files
            app.UseDefaultFiles();
            app.UseStaticFiles();
            #endregion

            //Run WebApplication.
            app.Run();
        }
    }
}