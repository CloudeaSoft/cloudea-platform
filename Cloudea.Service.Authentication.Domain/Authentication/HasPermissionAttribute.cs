using Cloudea.Entity.Base.Role;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Service.Auth.Domain.Authentication;

public sealed class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(Base_Permission permission)
        : base(policy: permission.ToString())
    {

    }
}
