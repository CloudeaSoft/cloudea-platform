using Cloudea.Infrastructure.Shared;
using Cloudea.Service.Auth.Domain.Entities;
using Cloudea.Service.Auth.Domain.Repositories;

namespace Cloudea.Service.Auth.Infrastructure
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IFreeSql _database;

        public RoleRepository(IFreeSql database)
        {
            _database = database;
        }

        public async Task<Result<Role>> GetRole(int roleId)
        {
            var res = await _database.Select<Role>().Where(x => x.Value == roleId).FirstAsync();
            return Result.Success(res);
        }

        public async Task<Result<Role>> GetRole(string roleName)
        {
            var res = await _database.Select<Role>().Where(x => x.Name == roleName).FirstAsync();
            return Result.Success(res);
        }

        public async Task<Result<List<Role>>> GetRoleList()
        {
            var res = await _database.Select<Role>().ToListAsync();
            return Result.Success(res);
        }

        public async Task<Result> SetRolePermission(Role role)
        {
            var res = await _database.Update<Role>(role).ExecuteAffrowsAsync();
            return Result.Success();
        }
    }
}
