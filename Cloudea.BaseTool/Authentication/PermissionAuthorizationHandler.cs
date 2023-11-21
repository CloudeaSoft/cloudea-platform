using Cloudea.Service.Base.Authentication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;
using Org.BouncyCastle.OpenSsl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Service.Base.Authentication;

public class PermissionAuthorizationHandler
    : AuthorizationHandler<PermissionRequirement>
{
    private readonly IServiceScopeFactory _scopeFactory;

    public PermissionAuthorizationHandler(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task<Task> HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        /*string? userIdClaim = context.User.Claims.FirstOrDefault(
            t => t.Type == JwtClaims.USER_ID)?.Value;

        if (!long.TryParse(userIdClaim, out long parsedUserId)) {
            return;
        }*/

        /*using IServiceScope scope = _scopeFactory.CreateScope();

        IPermissionService permissionService = scope.ServiceProvider
            .GetService<IPermissionService>();

        HashSet<string> permissions = await permissionService
            .GetPermissionsAsync(parsedUserId);*/
        HashSet<string> permissions = context
            .User
            .Claims
            .Where(x => x.Type == JwtClaims.USER_PERMISSIONS)
            .Select(x => x.Value)
            .ToHashSet();
        foreach (var permission in permissions) {
            await Console.Out.WriteLineAsync(permission.ToString());
        }

        if (permissions.Contains(requirement.Permission)) {
            context.Succeed(requirement);
        }
        return Task.CompletedTask;
    }
}
