using Cloudea.Infrastructure;
using Cloudea.Service.Auth.Domain;
using Cloudea.Service.Auth.Domain.Abstractions;
using Cloudea.Service.Auth.Domain.Authentication;
using Cloudea.Service.Auth.Domain.Role;
using Cloudea.Service.Auth.Domain.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Cloudea.Service.Auth.Infrastructure
{
    internal class ModuleInitializer : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddScoped<UserDomainService>();
            services.AddScoped<UserService>();
            services.AddScoped<VerificationCodeService>();
            services.AddScoped<AuthUserService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddSingleton<RoleService>();
            services.AddSingleton<UserRoleService>();
            services.AddSingleton<RolePermissionService>();
            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
            services.AddSingleton<IPermissionService, PermissionService>();
        }
    }
}
