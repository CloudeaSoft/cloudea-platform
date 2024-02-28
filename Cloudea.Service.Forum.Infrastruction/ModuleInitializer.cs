using Cloudea.Infrastructure;
using Cloudea.Service.Forum.Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Cloudea.Service.Forum
{
    public class ModuleInitializer : IModuleInitializer {
        public void Initialize(IServiceCollection services) {
            /// Entity
            services.AddScoped<IValidator<ForumPost>, ForumPostValidator>();
        }
    }
}
