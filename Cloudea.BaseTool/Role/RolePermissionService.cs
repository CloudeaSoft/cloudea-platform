using Cloudea.Entity.Base.Role;
using Cloudea.Infrastructure.Database;
using Cloudea.Infrastructure.Models;
using Cloudea.Service.Base.Authentication;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Service.Base.Role
{
    public class RolePermissionService : BaseCurdService<Base_Role_Permission>
    {
        private readonly IFreeSql _database;
        private readonly RoleService _roleService;

        public RolePermissionService(IFreeSql freeSql, RoleService roleService) : base(freeSql)
        {
            _database = freeSql;
            _roleService = roleService;
        }

        public override async Task<Result<long>> Create(Base_Role_Permission entity)
        {
            if (Enum.ToObject(typeof(Base_Permission), entity.PermissionId) is false) {
                return Result.Fail("权限不存在");
            }
            if ((await _roleService.Exist(entity.RoleId)) is false) {
                return Result.Fail("角色不存在");
            }

            var res = await base.Create(entity);

            return res;
        }

        public async Task<Result<List<int>>> ReadPermissionByRoleId(int roleId)
        {
            var PermissionIdList = await _database.Select<Base_Role_Permission>()
                .Where(x => x.RoleId == roleId)
                .ToListAsync(b => b.PermissionId).ConfigureAwait(true);

            return Result.Success(PermissionIdList);
        }

        public async Task<Result> Delete(Base_Role_Permission entity)
        {
            var res = _database.Delete<Base_Role_Permission>()
                .Where(x =>
                x.RoleId == entity.RoleId &&
                x.PermissionId == entity.PermissionId).ExecuteAffrowsAsync();

            return Result.Success();
        }
    }
}
