using Cloudea.Infrastructure.Database;
using Cloudea.Infrastructure.Models;
using Cloudea.Service.Auth.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Service.Auth.Infrastructure
{
    public class RoleDbContext
    {
        private readonly IFreeSql _database;

        public RoleDbContext(IFreeSql database)
        {
            _database = database;
        }

        public async Task<Result> Create(Role role)
        {
            long id = await _database.Insert(role).ExecuteAffrowsAsync();
            return Result.Success(id);
        }

        public async Task<Result> Update(Role newRole)
        {
            await _database.Update<Role>().SetSource(newRole).ExecuteAffrowsAsync();
            return Result.Success();
        }
    }
}
