using Cloudea.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Cloudea.Service.Forum
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddScoped<ForumTopicService>();
            services.AddScoped<ForumSectionService>();
            services.AddScoped<ForumReplyService>();
        }
    }
}
