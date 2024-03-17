using Cloudea.Domain.Common;
using Cloudea.Domain.GameHelper.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Domain.GameHelper
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddScoped<IArkNightsService, ArkNightsService>();
            services.AddScoped<IOsuService, OsuService>();
        }
    }
}
