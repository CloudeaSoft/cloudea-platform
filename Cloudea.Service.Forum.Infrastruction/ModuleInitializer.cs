using Cloudea.Entity.Forum;
using Cloudea.Infrastructure;
using Cloudea.Service.Forum.Domain;
using Cloudea.Service.Forum.Domain.Models;
using Cloudea.Service.Forum.Domain.Repositories;
using Cloudea.Service.Forum.Infrastruction;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Cloudea.Service.Forum
{
    public class ModuleInitializer : IModuleInitializer {
        public void Initialize(IServiceCollection services) {
            /// Application
            services.AddScoped<ForumApplicationService>();
            /// Domain Service
            services.AddScoped<ForumDomainService>();
            /// Repository
            services.AddScoped<IForumTopicRepository, ForumTopicRepository>();
            services.AddScoped<IForumSectionRepository, ForumSectionRepository>();
            services.AddScoped<IForumRepository, ForumRepository>();
            /// Entity
            services.AddScoped<IValidator<Forum_Topic>, Forum_Topic_Validator>();
            /// DTO
            services.AddScoped<IValidator<PostSectionRequest>, PostSectionRequest_Validator>();
            services.AddScoped<IValidator<PostTopicRequest>, PostTopicRequest_Validator>();
        }
    }
}
