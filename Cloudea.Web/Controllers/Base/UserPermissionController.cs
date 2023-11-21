using Cloudea.Entity.Base.Role;
using Cloudea.Infrastructure.Models;
using Cloudea.Service.Base.Role;
using Cloudea.Web.Utils.ApiBase;
using Microsoft.AspNetCore.Mvc;


namespace Cloudea.Web.Controllers.Base
{
    /// <summary>
    /// 
    /// </summary>
    public class UserPermissionController : ApiControllerBase
    {
        private readonly RoleService _roleService;
        private readonly RolePermissionService _rolePermissionService;

        public UserPermissionController(RoleService roleService, RolePermissionService rolePermissionService)
        {
            _roleService = roleService;
            _rolePermissionService = rolePermissionService;
        }

        /// <summary>
        /// 创建角色与权限关联
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> RolePermission(int roleId)
        {
            var res = await _rolePermissionService.ReadPermissionByRoleId(roleId);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> RolePermission(Base_Role_Permission newRolePermission)
        {
            var res = await _rolePermissionService.Create(newRolePermission);
            return Ok(res);
        }

        [HttpDelete]
        public async Task<IActionResult> RolePermissionDelete(Base_Role_Permission deleteRolePermission)
        {
            var res = await _rolePermissionService.Delete(deleteRolePermission);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> Role()
        {
            var res = await _roleService.Init().ConfigureAwait(true);
            if (res.IsFailure()) {
                return BadRequest(res);
            }
            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> Permission()
        {
            var res = Enum.GetValues(typeof(Base_Permission)).OfType<string>().ToList();

            return Ok(res);
        }
    }
}
