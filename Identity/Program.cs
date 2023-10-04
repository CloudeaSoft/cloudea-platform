using Cloudea.Infrastructure.Db;
using Identity.Entity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDataBaseDefault(
    FreeSql.DataType.MySql,
    @"Server=localhost;Port=3306;Database=test;Uid=root;Pwd=123456;"
);

builder.Services.AddAuthentication();

builder.Services.AddFreeSqlIdentity(builder.Configuration);

builder.Services.AddDataProtection();

builder.Services.Configure<IdentityOptions>(options => {
    // Password
    options.Password.RequiredLength = 6; // The minimum length a password must be. Defaults to 6.
    options.Password.RequiredUniqueChars = 1; // The minimum number of unique characters which a password must contain.Defaults to 1.
    options.Password.RequireDigit = false; // If passwords must contain a digit.
    options.Password.RequireLowercase = false; // If passwords must contain a lower case ASCII character. Defaults to true.
    options.Password.RequireUppercase = false; // If passwords must contain a upper case ASCII character. Defaults to true.
    options.Password.RequireNonAlphanumeric = false; // If passwords must contain a non-alphanumeric character. Defaults to true.

    // Token
    options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
    options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;

    // User
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_";
    options.User.RequireUniqueEmail = false;
});

builder.Services.ConfigureApplicationCookie(options => {
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
        options => builder.Configuration.Bind("JwtSettings", options))
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
        options => builder.Configuration.Bind("CookieSettings", options));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Services.RunScopeService();

app.Run();
