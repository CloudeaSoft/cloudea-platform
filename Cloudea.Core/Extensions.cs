using Cloudea.Core.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration.Json;
using Serilog.Events;
using System;

namespace Cloudea.Core
{
    public static class Extensions
    {
        public static IHostBuilder UseJst(this IHostBuilder builder)
        {
            ConfigurationBuilder configBuilder = new ConfigurationBuilder();
            configBuilder.AddJsonFile("appsettings.json");
            var config = configBuilder.Build();
            var logSection = config.GetSection("Logging:Path");




            // 日志等级
            string level = config.GetSection("Logging:MinimumLevel").Value;
            LogEventLevel lv = LogEventLevel.Information;
            if (Enum.TryParse(level, out LogEventLevel minLv))
            {
                lv = minLv;
            }

            // 日志
            builder = Logger.CreateLog(builder, logSection.Value, lv);
            // 依赖注入
            builder = DependcyInjection.Create(builder);
            return builder;
        }
    }
}
