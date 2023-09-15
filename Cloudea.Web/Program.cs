using Cloudea.Core;
using Cloudea.Infrastructure.Db;
using Cloudea.Web.Utils.ApiBase;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Configuration;
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
            // 在控制台打印图标
            OutputFile.outputTxt(builder.Configuration);

            #region 依赖注入 Add services to the container. Use Configuration to config the services.

            // 控制器
            builder.Services.AddControllers(options => { //添加约定器，对ApiConventionController的派生类添加路由前缀
                options.Conventions.Add(new NamespaceRouteControllerModelConvention("/api"));
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options => {
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

            // Http请求
            builder.Services.AddHttpClient();

            // 跨域配置
            builder.Services.AddCors(opt => {
                opt.AddDefaultPolicy(b => {
                    b.WithOrigins(new string[] { "http://localhost:1111" })
                    //.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                });
            });

            // Service 中的服务类
            builder.Services.RunModuleInitializers();

            // Serilog配置
            builder.Host.UseSerilog((context, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration));

            // Freesql
            builder.Services.AddDataBaseDefault(
                    FreeSql.DataType.MySql,
                    @"Server=localhost;Port=3306;Database=test;Uid=root;Pwd=123456;");

            #endregion

            //Build Webapplication.
            var app = builder.Build();

            #region 装配中间件管道 Configure the HTTP request pipeline.

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

            #endregion

            //Run webapplication.
            app.Run();
        }
    }
}