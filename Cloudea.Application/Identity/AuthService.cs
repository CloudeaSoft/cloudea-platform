using Cloudea.Domain.Common.Shared;
using Cloudea.Domain.Identity.Entities;
using Cloudea.Infrastructure.Repositories;
using Cloudea.Infrastructure.Shared;
using Cloudea.Service.Auth.Domain.Repositories;

namespace Cloudea.Application.Identity
{
    public class AuthService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(IRoleRepository authRepository, IUnitOfWork unitOfWork)
        {
            _roleRepository = authRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 获取Role的列表
        /// </summary>
        /// <returns></returns>
        public async Task<Result<PageResponse<Role>>> GetRoleListAsync(PageRequest request, CancellationToken cancellationToken = default)
        {
            var res = await _roleRepository.GetPageAsync(request, cancellationToken);
            if (res == null) {
                return new Error("角色不存在");
            }
            return res;
        }

        /// <summary>
        /// 修改Role的Permission信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="permissions"></param>
        /// <returns></returns>
        public async Task<Result<Role>> SetRolePermissionAsync(int roleId, List<Permission> permissions)
        {
            Role? role = await _roleRepository.GetByIdAsync(roleId);
            if (role is null) {
                return new Error("角色不存在");
            }
            role.Permissions = permissions;
            return role;
        }

        public async Task<Result<Role>> CreateRoleAsync(string name, List<Permission> permissions, CancellationToken cancellationToken = default)
        {
            var checkIfExist = await _roleRepository.GetByNameAsync(name);
            if (checkIfExist is not null) {
                return new Error("角色已存在");
            }

            var newRole = new Role(1, "Admin") {
                Permissions = permissions
            };
            _roleRepository.Add(newRole);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return newRole;
        }
    }
}
