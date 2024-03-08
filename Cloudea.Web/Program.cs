using Cloudea.Infrastructure;
using Cloudea.Infrastructure.Freesql;
using Cloudea.Infrastructure.Repositories;
using Cloudea.Persistence;
using Cloudea.Persistence.BackgroundJobs;
using Cloudea.Service.HubTest;
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

namespace Cloudea.Web
{
    /// <summary>
    /// 主程序
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 主程序入口
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            // Init Builder
            var builder = WebApplication.CreateBuilder(args);

            // 在控制台打印图标
            OutputFile.OutputTxt(builder.Configuration);

            #region Add services to the container. Use Configuration to config the services.
            // Appsettings.json to Configurations
            builder.Services.ConfigureOptions<DatabaseOptionsSetup>();
            builder.Services.ConfigureOptions<JwtOptionsSetup>();
            builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();
            builder.Services.ConfigureOptions<MailOptionsSetup>();

            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();

            // 控制器
            builder.Services
                .AddControllers(options => {
                    // 添加过滤器
                })
                .AddJsonOptions(options => {
                    options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            // Swagger接口文档
            builder.Services.AddSwaggerGen(options => {
                // 描述文档
                options.SwaggerDoc("v1", new OpenApiInfo {
                    Version = "v1",
                    Title = "Cloudea API",
                    Description = "施工中，还请耐心等候",
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

                // using System.Reflection, 自动生成接口注释文档
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

                // 验证
                #region 启用验证功能，并添加鉴权的描述
                var openApiSecurity = new OpenApiSecurityScheme {
                    Name = "Authorization", //jwt 默认参数名称
                    Description = "JWT认证授权，使用直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,  //jwt默认存放Authorization信息的位置（请求头）
                    Reference = new OpenApiReference {
                        Type = ReferenceType.SecurityScheme,
                        Id = JwtBearerDefaults.AuthenticationScheme
                    }
                };

                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, openApiSecurity);
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                        {
                            { openApiSecurity, Array.Empty<string>() }
                        });
                #endregion
            });

            // MVC的相关服务
            builder.Services.AddMvc();

            // WebSocket服务 - SignalR
            builder.Services.AddSignalR();

            // 接口鉴权授权服务
            builder.Services
                .AddAuthentication(x => {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer((options) => {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters {
                        //NameClaimType = JwtClaimTypes.Name,
                        //RoleClaimType = JwtClaimTypes.Role,

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

            // Http请求
            builder.Services.AddHttpContextAccessor();

            // Http客户端
            builder.Services.AddHttpClient();

            // 内存缓存服务
            builder.Services.AddMemoryCache();

            // MediatR
            builder.Services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssemblies(AssemblyLoader.GetAllAssemblies());
            });

            // CORS配置
            builder.Services.AddCors(opt => {
                opt.AddDefaultPolicy(b => {
                    b.WithOrigins("http://localhost:5173")
                    //.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                });
            });

            // 注入各类库的服务
            builder.Services.RunModuleInitializers();

            // Serilog配置
            builder.Host.UseSerilog((context, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration));

            // Freesql
            builder.Services.AddDataBaseDefault(
                    FreeSql.DataType.MySql,
                    "Server=localhost;Port=3306;Database=test;Uid=root;Pwd=123456;");

            // 持久化 - Efcore
            builder.Services.AddDbContext<ApplicationDbContext>(
                (IServiceProvider serviceProvider, DbContextOptionsBuilder dbContextOptionsBuilder) => {
                    var databaseOptions = serviceProvider.GetService<IOptions<DatabaseOptions>>()!.Value;

                    dbContextOptionsBuilder.UseMySql(databaseOptions.ConnectionString, ServerVersion.Parse("8.0.26"), mysqlAction => {
                        mysqlAction.EnableRetryOnFailure(databaseOptions.MaxRetryCount);
                        mysqlAction.CommandTimeout(databaseOptions.CommandTimeout);
                    });

                    dbContextOptionsBuilder.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);

                    dbContextOptionsBuilder.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);
                });

            // 定时服务 - Quartz
            builder.Services.AddQuartz(configure => {
                var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));

                configure
                    .AddJob<ProcessOutboxMessagesJob>(jobKey)
                    .AddTrigger(
                        trigger =>
                            trigger.ForJob(jobKey)
                                .WithSimpleSchedule(
                                    schedule =>
                                        schedule.WithIntervalInSeconds(10)
                                            .RepeatForever()));
            });
            builder.Services.AddQuartzHostedService();
            #endregion

            //Build Webapplication.
            var app = builder.Build();

            #region 装配中间件管道 Configure the HTTP request pipeline.
            app.Logger.LogInformation("系统开始运行...");

#if DEBUG
            // 接口文档
            app.UseSwagger();
            app.UseSwaggerUI();

            // 重定向
            app.UseHttpsRedirection();

            // 错误处理
            app.UseExceptionHandler();
#else
            // 接口文档
            app.UseMiddleware<SwaggerAuthMiddleware>();
            app.UseSwagger();
            app.UseSwaggerUI();

            // 全局错误捕获
            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
#endif
            //允许X-HTTP-Method-Override属性
            app.UseHttpMethodOverride();

            // 路由
            app.UseRouting();
            // 跨域
            app.UseCors();

            // 控制器
            app.MapHub<ChatRoomHub>("/ChatRoomHub"); // Websocket服务器
            app.MapControllers();

            // 接口请求日志
            app.UseSerilogRequestLogging();

            // 认证与认证过滤器
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<UserLoginGuidAuthenticationMiddleware>();

            // 静态文件
            app.UseDefaultFiles();
            app.UseStaticFiles();
            #endregion

            //Run WebApplication.
            app.Run();
        }
    }
}