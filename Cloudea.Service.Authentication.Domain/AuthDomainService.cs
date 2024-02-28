using Cloudea.Infrastructure.Shared;
using Cloudea.Service.Auth.Domain.Entities;
using Cloudea.Service.Auth.Domain.Repositories;

namespace Cloudea.Service.Auth.Domain
{
    public class AuthDomainService
    {
        private readonly IRoleRepository _authRepository;

        public AuthDomainService(IRoleRepository authRepository)
        {
            _authRepository = authRepository;
        }

        /// <summary>
        /// 获取Role的列表
        /// </summary>
        /// <returns></returns>
        public async Task<Result<List<Role>>> GetRoleList()
        {
            var res = await _authRepository.GetRoleList();
            if (res == null)
            {
                return new Error("角色不存在");
            }
            var roles = res.Data;
            return roles;
        }

        /// <summary>
        /// 修改Role的Permission信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="permissions"></param>
        /// <returns></returns>
        public async Task<Result<Role>> SetRolePermission(int roleId, List<Permission> permissions)
        {
            var res = await _authRepository.GetRole(roleId);
            if (res == null)
            {
                return new Error("角色不存在");
            }
            var role = res.Data;
            role.Permissions = permissions;
            return role;
        }

        public async Task<Result<Role>> CreateRole(string name, List<Permission> permissions)
        {
            var res = await _authRepository.GetRole(name);
            if (res is not null)
            {
                return new Error("角色已存在");
            }
            var newRole = new Role(1, "Admin")
            {
                Permissions = permissions
            };
            return newRole;
        }
    }
}
