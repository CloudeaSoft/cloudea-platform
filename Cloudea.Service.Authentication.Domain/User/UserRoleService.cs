using Cloudea.Entity.Base.User;
using Cloudea.Infrastructure.Database;
using Cloudea.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Service.Auth.Domain.User
{
    public class UserRoleService : BaseCurdService<Base_User_Role>
    {
        private readonly IFreeSql _database;

        public UserRoleService(IFreeSql fsql) : base(fsql)
        {
            _database = fsql;
        }

        public async Task<Result<List<int>>> ReadByUserId(long userId)
        {
            var res = await _database
                .Select<Base_User_Role>()
                .Where(x => x.UserId == userId)
                .Select(x => x.RoleId)
                .ToListAsync()
                .ConfigureAwait(true);

            return Result.Success(res);
        }
    }
}
