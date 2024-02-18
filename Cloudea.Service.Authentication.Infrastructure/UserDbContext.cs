using Cloudea.Infrastructure.Database;
using Cloudea.Service.Auth.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Service.Auth.Infrastructure
{
    public class UserDbContext : BaseRepository<User>
    {
        public UserDbContext(IFreeSql database) : base(database)
        {
        }
    }
}
