using Microsoft.AspNetCore.Authorization;

namespace Cloudea.Service.Auth.Domain.Overrides
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionRequirement(string permission)
        {
            Permission = permission;
        }

        public string Permission { get; set; }
    }
}
