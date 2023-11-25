using Cloudea.Infrastructure;
using Cloudea.Service.Base.Jwt;
using Microsoft.Extensions.DependencyInjection;

namespace Cloudea.Service.Base
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddScoped<RegionClockService>();
            services.AddScoped<JwtTokenService>();
        }
    }
}
