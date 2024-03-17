using Cloudea.Application.Utils;
using Cloudea.Domain.Common;
using Cloudea.Domain.File.Infrastructure;
using Cloudea.Infrastructure.FileStorageClients;
using Cloudea.Infrastructure.Jwt;
using Microsoft.Extensions.DependencyInjection;

namespace Cloudea.Infrastructure;

internal class ModuleInitializer : IModuleInitializer
{
    public void Initialize(IServiceCollection services)
    {
        services.AddScoped<IStorageClient, DefaultStorageClient>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();
    }
}
