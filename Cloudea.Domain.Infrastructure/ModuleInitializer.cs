using Cloudea.Application.Utils;
using Cloudea.Domain.Infrastructure.FileStorageClients;
using Cloudea.Infrastructure;
using Cloudea.Service.Base.Jwt;
using Cloudea.Service.File.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Cloudea.Domain.Infrastructure;

internal class ModuleInitializer : IModuleInitializer
{
    public void Initialize(IServiceCollection services)
    {
        services.AddScoped<IStorageClient, DefaultStorageClient>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();
    }
}
