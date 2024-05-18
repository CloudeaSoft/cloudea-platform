using Cloudea.Application.Forum;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Cloudea.Application.Identity;
using Cloudea.Application.Abstractions;
using Cloudea.Domain.Common;
using Cloudea.Application.File;

namespace Cloudea.Application
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddScoped<ForumService>();
            services.AddScoped<ForumRecommendService>();

            services.AddScoped<AuthService>();

            services.AddScoped<IdentityService>();
            services.AddScoped<UserService>();
            services.AddScoped<ICurrentUser, CurrentUser>();
            services.AddScoped<VerificationCodeService>();

            services.AddScoped<FileService>();

            services.AddScoped<RegionClockService>();
        }
    }
}
