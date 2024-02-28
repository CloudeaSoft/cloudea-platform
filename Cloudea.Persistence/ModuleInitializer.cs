using Cloudea.Infrastructure;
using Cloudea.Infrastructure.Repositories;
using Cloudea.Persistence.Repositories.Forum;
using Cloudea.Service.Forum.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Cloudea.Persistence
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IForumSectionRepository, ForumSectionRepository>();
            services.AddScoped<IForumPostRepository, ForumPostRepository>();
            services.AddScoped<IForumReplyRepository, ForumReplyRepository>();
        }
    }
}
