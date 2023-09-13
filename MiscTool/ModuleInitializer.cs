using Cloudea.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Cloudea.MiscTool {
    public class ModuleInitializer : IModuleInitializer {
        public void Initialize(IServiceCollection services) {
            services.AddScoped<RegionClock>();
            services.AddScoped<FileManager>();
        }
    }
}