using Cloudea.Infrastructure.Database;
using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Service.Auth.Domain.Entities;

public enum Permission
{
    AccessMember = 1,
    ReadMember = 2,
    Test = 3
}
