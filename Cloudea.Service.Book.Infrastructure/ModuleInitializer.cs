using Cloudea.Infrastructure;
using Cloudea.Service.Book.Domain;
using Cloudea.Service.Book.Domain.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Cloudea.Service.Book.Infrastructure
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddScoped<BookDomainService>();

            services.AddScoped<IMetaRepository, MetaRepository>();

            services.AddScoped<MetaDbContext>();
        }
    }
}
