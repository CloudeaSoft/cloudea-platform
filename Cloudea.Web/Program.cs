using Cloudea.Core;
using Cloudea.Infrastructure.Db;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace Cloudea.Web {
    public class Program {
        public static void Main(string[] args) {
            // Init Builder
            var builder = WebApplication.CreateBuilder(args);

            // Configuration Source: appsettings.json. // Auto injected by .NetCore frame
            IConfiguration Configuration = builder.Configuration;
            Console.WriteLine(Configuration["Cloudea:Name"]);

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
                builder.Services.AddSwaggerGen();

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
                builder.Services.AddControllers() // services.AddMvc»òÕßservices.AddControllersWithViews()
      .AddXmlDataContractSerializerFormatters();

                //自定义自动注入 通过扫描得到的类
                {
                    var asms = ReflectionHelper.GetAllReferencedAssemblies();
                    foreach (var asm in asms) {
                        foreach (var a in asm.GetTypes()) {
                            Console.WriteLine(a);
                        }
                    }
                    builder.Services.RunModuleInitializers(asms);
                }
            }

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

            }

            //Run webapplication.
            app.Run();
        }
    }
}