using Cloudea.Infrastructure;
using Cloudea.Service.File.Domain;
using Cloudea.Service.File.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Service.File.Infrastructure;

internal class ModuleInitializer : IModuleInitializer
{
    public void Initialize(IServiceCollection services)
    {
        services.AddScoped<IFSRepository, FSRepository>();
        services.AddScoped<IStorageClient, DefaultStorageClient>();
        services.AddScoped<FSDomainService>();
    }
}
