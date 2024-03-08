using Cloudea.Domain.Identity.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Cloudea.Service.Auth.Domain.Attributes;

public sealed class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(Permission permission)
        : base(policy: permission.ToString())
    {

    }
}
