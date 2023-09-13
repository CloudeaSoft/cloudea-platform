using Cloudea.Core;
using Cloudea.Entity.MiscTool;
using Cloudea.MiscTool;
using Cloudea.MyService;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.MyService
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddScoped<FileManager>();
            services.AddScoped<OsuHelper>();
            services.AddScoped<RegionClock>();
        }
    }
}
