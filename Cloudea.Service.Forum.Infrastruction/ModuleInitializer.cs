using Cloudea.Entity.Forum;
using Cloudea.Infrastructure;
using Cloudea.Service.Forum.Domain;
using Cloudea.Service.Forum.Domain.Models;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Cloudea.Service.Forum
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            /// Application
            services.AddScoped<ForumApplicationService>();
            /// Domain Service
            services.AddScoped<ForumDomainService>();
            /// Repository
            services.AddScoped<IForumTopicRepository, ForumTopicRepository>();
            services.AddScoped<IForumSectionRepository, ForumSectionRepository>();
            services.AddScoped<ForumReplyRepository>();
            /// Entity
            services.AddScoped<IValidator<Forum_Topic>, Forum_Topic_Validator>();
            /// DTO
            services.AddScoped<IValidator<PostTopicRequest>, PostTopicRequest_Validator>();
        }
    }
}
