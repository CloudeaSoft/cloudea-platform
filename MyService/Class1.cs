using Cloudea.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyService
{
    public class Class1 : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddScoped<My1Service>();
        }
    }
}
