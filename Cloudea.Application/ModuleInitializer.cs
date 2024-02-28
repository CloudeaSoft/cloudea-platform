using Cloudea.Application.Forum;
using Cloudea.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace Cloudea.Application
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddScoped<ForumService>();
        }
    }
}
