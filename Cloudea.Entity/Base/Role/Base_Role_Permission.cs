using Cloudea.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Entity.Base.Role
{
    [AutoGenerateTable]
    public class Base_Role_Permission : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int RoleId {  get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int PermissionId { get; set; }
    }
}
