using Cloudea.Core;
using Cloudea.Infrastructure.Db;
using Microsoft.Extensions.DependencyInjection;
using MyService;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace Cloudea.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMvc().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

            builder.Services.AddCors(opt =>
            {
                opt.AddDefaultPolicy(b =>
                {
                    b.WithOrigins(new string[] { "http://localhost:5173" })
                    //.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                });
            });

            builder.Services.AddScoped<My1Service>();
            
            builder.Services.AddControllers() // 也可是services.AddMvc或者services.AddControllersWithViews()
  .AddXmlDataContractSerializerFormatters();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors();

            app.UseDefaultFiles();
            app.UseStaticFiles();      

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}