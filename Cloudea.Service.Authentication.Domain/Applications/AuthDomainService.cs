using Cloudea.Infrastructure.Models;
using Cloudea.Service.Auth.Domain.Abstractions;
using Cloudea.Service.Auth.Domain.Entities;

namespace Cloudea.Service.Auth.Domain.Applications
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
                return Result.Fail();
            }
            var roles = res.Data;
            return Result.Success(roles);
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
                return Result.Fail("角色不存在");
            }
            var role = res.Data;
            role.Permissions = permissions;
            return Result.Success(role);
        }

        public async Task<Result<Role>> CreateRole(string name, List<Permission> permissions)
        {
            var res = await _authRepository.GetRole(name);
            if (res is not null)
            {
                return Result.Fail("已存在");
            }
            var newRole = new Role(1, "Admin");
            newRole.Permissions = permissions;
            return Result.Success(newRole);
        }
    }
}
