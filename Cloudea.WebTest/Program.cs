using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Serilog;
using Serilog.Events;
using Cloudea.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Cloudea.WebTest.Filter;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .CreateLogger();

try {
    Log.Information("Starting web application");
    //Log.Debug("Debug");
    //Log.Warning("Warning");
    //Log.Error("Error");

    var builder = WebApplication.CreateBuilder(args);

    var con = builder.Configuration.GetSection("ConnStr").Value;
    Console.WriteLine(con);

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.Configure<MvcOptions>(opt => {
        opt.Filters.Add<ActionFilter1>();
        opt.Filters.Add<ActionFilter2>();
    });

    builder.Services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
            options => builder.Configuration.Bind("JwtSettings", options))
        .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
            options => builder.Configuration.Bind("CookieSettings", options));

    Func<IServiceProvider, IFreeSql> fsqlFactory = r => {
        IFreeSql fsql = new FreeSql.FreeSqlBuilder()
            .UseConnectionString(FreeSql.DataType.MySql, @"Server=localhost;Port=3306;Database=test;Uid=root;Pwd=123456;")
            .UseMonitorCommand(cmd => Console.WriteLine($"Sql：{cmd.CommandText}"))//监听SQL语句
            .UseAutoSyncStructure(true) //自动同步实体结构到数据库，FreeSql不会扫描程序集，只有CRUD时才会生成表。
            .Build();
        return fsql;
    };
    builder.Services.AddSingleton<IFreeSql>(fsqlFactory);

    builder.Host.UseSerilog();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment()) {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    

    app.UseSerilogRequestLogging();

    app.UseHttpsRedirection();

    app.UseAuthentication();//<------------------

    app.UseAuthorization();

    app.MapControllers();

    app.UseStaticFiles();

    app.Run();
}
catch (Exception ex) {
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally {
    Log.CloseAndFlush();
}