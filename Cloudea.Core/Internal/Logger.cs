using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.IO;

namespace Cloudea.Core.Internal
{
    internal static class Logger
    {
        internal static IHostBuilder CreateLog(IHostBuilder builder, string logPath = "", LogEventLevel miniumLevel = LogEventLevel.Information)
        {

            string basePath = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
            string path = "";
            //  判断是否是绝对路径
            if (Path.IsPathRooted(logPath))
            {
                path = logPath;
            }
            else
            {
                path = Path.GetFullPath(Path.Combine(basePath, logPath));
            }


            string logCombinePath = path + "/" + "log.txt";

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Is(miniumLevel)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Hosting.Diagnostics", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(logCombinePath,
                rollingInterval: RollingInterval.Day,
                outputTemplate: "{Timestamp:HH:mm:ss} || {Level} || {SourceContext:l} || {Message} || {Exception} ||end {NewLine} ",
                retainedFileCountLimit: 60)// 保存60天的日志
                .CreateLogger();

            return builder.UseSerilog(Log.Logger);
        }
    }
}