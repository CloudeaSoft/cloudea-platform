using Microsoft.Extensions.DependencyInjection;
using Cloudea.Infrastructure;

namespace Cloudea.Service.BookShelf
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddScoped<BookService>();
        }
    }
}
