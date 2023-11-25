using Cloudea.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Service.Auth.Domain.Entities;

[AutoGenerateTable]
public enum Auth_Permission
{
    AccessMember = 1,
    ReadMember = 2,
    Test = 3
}
