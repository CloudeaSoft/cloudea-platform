using Cloudea.Infrastructure;
using Cloudea.Service.Forum.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Cloudea.Service.Forum
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddScoped<ForumService>();

            services.AddScoped<ForumDomainService>();

            services.AddScoped<IForumTopicRepository, ForumTopicRepository>();
            services.AddScoped<IForumSectionRepository, ForumSectionRepository>();
            services.AddScoped<ForumReplyRepository>();

        }
    }
}
