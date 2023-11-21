using Cloudea.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Entity.Base.Role;

[AutoGenerateTable]
public enum Base_Permission
{
    AccessMember = 1,
    ReadMember = 2,
    Test = 3
}
