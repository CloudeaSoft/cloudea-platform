using Cloudea.Domain.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Cloudea.Domain.Identity.Overrides;

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
        HashSet<string> permissions = context
            .User
            .Claims
            .Where(x => x.Type == JwtClaims.USER_PERMISSIONS)
            .Select(x => x.Value)
            .ToHashSet();

        if (permissions.Contains(requirement.Permission)) {
            context.Succeed(requirement);
        }
        return Task.CompletedTask;
    }
}
