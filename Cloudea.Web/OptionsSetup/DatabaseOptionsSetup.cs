using Cloudea.Infrastructure.Repositories;
using Microsoft.Extensions.Options;

namespace Cloudea.Web.OptionsSetup;

public class DatabaseOptionsSetup : IConfigureOptions<DatabaseOptions>
{
    private const string ConfigurationSectionName = "DatabaseOptions";

    private readonly IConfiguration _configuration;

    public DatabaseOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(DatabaseOptions options)
    {
        var connectionString = _configuration.GetConnectionString("Database") ?? 
            throw new NotImplementedException("未在appsettings.json中添加数据库连接字符串信息");
        
        options.ConnectionString = connectionString;

        _configuration.GetSection(ConfigurationSectionName).Bind(options);
    }
}
