using Cloudea.Core;
using Cloudea.Service.Base.Authentication;
using Cloudea.Service.Base.File;
using Cloudea.Service.Base.Role;
using Cloudea.Service.Base.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Cloudea.Service.Base
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddScoped<FileService>();
            services.AddScoped<RegionClockService>();
            services.AddScoped<UserService>();
            services.AddScoped<VerificationCodeService>();
            services.AddScoped<JwtTokenService>();
            services.AddScoped<AuthUserService>();
            services.AddScoped<CurrentUserService>();
            services.AddSingleton<RoleService>();
            services.AddSingleton<UserRoleService>();
            services.AddSingleton<RolePermissionService>();
            services.AddSingleton<IAuthorizationHandler,PermissionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
            services.AddSingleton<IPermissionService,Authentication.PermissionService>();
        }
    }
}
