using Cloudea.Application.Forum;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Cloudea.Application.Identity;
using Cloudea.Application.Abstractions;
using Cloudea.Domain.Common;

namespace Cloudea.Application
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddScoped<ForumService>();
            services.AddScoped<AuthService>();

            services.AddScoped<UserService>();
            services.AddScoped<UserProfileService>();
            services.AddScoped<ICurrentUser, CurrentUser>();
            services.AddScoped<VerificationCodeService>();
        }
    }
}
