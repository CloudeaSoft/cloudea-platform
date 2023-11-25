using Cloudea.Entity.Base.Role;
using Cloudea.Infrastructure.Database;
using Cloudea.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Service.Auth.Domain.Role
{
    public class RoleService
    {
        private readonly IFreeSql _database;

        public RoleService(IFreeSql freeSql)
        {
            _database = freeSql;
        }

        /// <summary>
        /// 弃用
        /// </summary>
        /// <param name="newRole"></param>
        /// <returns></returns>
        public async Task<Result> Create(Base_Role newRole)
        {
            var res = await _database.Insert(newRole).ExecuteAffrowsAsync();
            return Result.Success(res);
        }

        public async Task<Result> Init()
        {
            var res = await _database.InsertOrUpdate<Base_Role>()
                .SetSource(new Base_Role[] {
                    Base_Role.User,
                    Base_Role.Admin,
                    Base_Role.SubAdmin
                })
                //.IfExistsDoNothing()
                .ExecuteAffrowsAsync().ConfigureAwait(true);

            if (res <= 0) {
                return Result.Fail("插入失败");
            }

            return Result.Success();
        }

        public async Task<Result> Read()
        {
            var res = await _database.Select<Base_Role>().ToListAsync();
            return Result.Success(res);
        }

        public async Task<bool> Exist(int roleId)
        {
            return await _database.Select<Base_Role>().Where(t => t.Value == roleId).AnyAsync();
        }
    }
}
