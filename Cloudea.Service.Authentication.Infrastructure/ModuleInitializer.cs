using Cloudea.Infrastructure;
using Cloudea.Service.Auth.Domain;
using Cloudea.Service.Auth.Domain.Abstractions;
using Cloudea.Service.Auth.Domain.Applications;
using Cloudea.Service.Auth.Domain.Overrides;
using Cloudea.Service.Auth.Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Cloudea.Service.Auth.Infrastructure
{
    internal class ModuleInitializer : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            // Utils
            services.AddScoped<VerificationCodeService>();

            // Overrides
            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

            // Domain Service / Repository / DbContext
            services.AddScoped<UserDomainService>();
            services.AddScoped<AuthDomainService>();
            services.AddScoped<ICurrentUser, CurrentUser>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IUserLoginRepository, UserLoginRepository>();

            services.AddScoped<UserDbContext>();
            services.AddScoped<RoleDbContext>();
        }
    }
}
