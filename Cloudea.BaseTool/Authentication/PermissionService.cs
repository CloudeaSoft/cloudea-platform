using Cloudea.Entity.Base.Role;
using Cloudea.Service.Base.Role;
using Cloudea.Service.Base.User;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Service.Base.Authentication
{
    public class PermissionService : IPermissionService
    {
        private readonly UserRoleService _userRoleService;
        private readonly RolePermissionService _rolePermissionService;

        public PermissionService(UserRoleService userRoleService, RolePermissionService rolePermissionService)
        {
            _userRoleService = userRoleService;
            _rolePermissionService = rolePermissionService;
        }

        public async Task<HashSet<string>> GetPermissionsAsync(long userId)
        {
            // 获取用户Role列表
            var roles = await _userRoleService.ReadByUserId(userId);

            // 获取用户Permission列表
            ICollection<Base_Permission> permissionIds = new List<Base_Permission>();
            foreach (var role in roles.Data) {
                var permissionList = await _rolePermissionService.ReadPermissionByRoleId(role);
                foreach (var permission in permissionList.Data) {
                    permissionIds.Add((Base_Permission)Enum.ToObject(typeof(Base_Permission), permission));
                }
            }
            var res = from permission in permissionIds
                      select permission.ToString();

            return res.ToHashSet();
        }
    }
}
