using Cloudea.Domain.Identity.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Cloudea.Domain.Identity.Attributes;

public sealed class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(Permission permission)
        : base(policy: permission.ToString())
    {

    }
}
