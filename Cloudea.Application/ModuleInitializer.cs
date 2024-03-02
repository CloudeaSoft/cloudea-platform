using Cloudea.Application.Forum;
using Cloudea.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Cloudea.Application.Identity;
using Cloudea.Service.Auth.Domain.Abstractions;
using Cloudea.Service.Auth.Domain.Utils;

namespace Cloudea.Application
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddScoped<ForumService>();
            services.AddScoped<AuthService>();

            services.AddScoped<UserService>();
            services.AddScoped<ICurrentUser, CurrentUser>();
            services.AddScoped<VerificationCodeService>();
        }
    }
}
