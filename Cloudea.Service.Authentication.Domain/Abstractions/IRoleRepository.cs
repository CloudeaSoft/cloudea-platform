using Cloudea.Infrastructure.Models;
using Cloudea.Service.Auth.Domain.Entities;

namespace Cloudea.Service.Auth.Domain.Abstractions
{
    public interface IRoleRepository
    {
        Task<Result<Role>> GetRole(int roleId);
        Task<Result<Role>> GetRole(string roleName);
        Task<Result<List<Role>>> GetRoleList();
        Task<Result> SetRolePermission(Role role);
    }
}
