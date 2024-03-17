using Microsoft.AspNetCore.Authorization;

namespace Cloudea.Domain.Identity.Overrides
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
