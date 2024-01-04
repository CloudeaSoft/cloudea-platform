using Cloudea.Infrastructure.Models;
using Cloudea.Service.Auth.Domain.Abstractions;
using Cloudea.Service.Auth.Domain.Entities;

namespace Cloudea.Service.Auth.Domain.Applications
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly IFreeSql _database;

        public UserRoleRepository(IFreeSql database)
        {
            _database = database;
        }

        public async Task<Result<List<int>>> ReadByUserId(Guid userId)
        {
            var res = await _database
                .Select<UserRole>()
                .Where(x => x.UserId == userId)
                .Select(x => x.RoleId)
                .ToListAsync()
                .ConfigureAwait(true);

            return Result.Success(res);
        }
    }
}
