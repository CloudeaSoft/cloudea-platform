using Cloudea.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Entity.Base.User
{
    [AutoGenerateTable]
    public class Base_User_Role : BaseEntity
    {
        public long UserId { get; set; }
        public int RoleId { get; set; }
    }
}
