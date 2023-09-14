using Cloudea.Core;
using Cloudea.Infrastructure.Db;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace Cloudea.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Init Builder
            var builder = WebApplication.CreateBuilder(args);

            // Configuration Source: appsettings.json. // Auto injected by .NetCore frame
            OutputFile.outputTxt(builder.Configuration);

            // Add services to the container. Use Configuration to config the services.
            {
                //注入 Freesql
                builder.Services.AddDataBaseDefault(
                    FreeSql.DataType.MySql,
                    @"Server=localhost;Port=3306;Database=test;Uid=root;Pwd=123456;"
                );

                //注入 控制器
                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo {
                        Version = "v1",
                        Title = "ToDo API",
                        Description = "An ASP.NET Core Web API for managing ToDo items",
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

                    // using System.Reflection;
                    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                });

                builder.Services.AddMvc().AddJsonOptions(options => {
                    options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                });

                //注入 跨域配置
                builder.Services.AddCors(opt => {
                    opt.AddDefaultPolicy(b => {
                        b.WithOrigins(new string[] { "http://localhost:5173" })
                        //.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                    });
                });

                //注入 XML分析
                // builder.Services.AddControllers(); // services.AddMvc»òÕßservices.AddControllersWithViews()
                                                   //.AddXmlDataContractSerializerFormatters();

                //自动注入 Service 中的类
                {
                    var asms = ReflectionHelper.GetAllReferencedAssemblies();
                    builder.Services.RunModuleInitializers(asms);
                }
            }

            builder.Host.UseSerilog((context, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration));

            //Build webapplication.
            var app = builder.Build();

            //Add middlewares to pipeline.
            {
                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment()) {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }
                app.UseCors();

                app.UseDefaultFiles();
                app.UseStaticFiles();

                app.UseAuthorization();

                app.MapControllers();

                app.UseSerilogRequestLogging();
            }

            //Run webapplication.
            app.Run();
        }
    }
}