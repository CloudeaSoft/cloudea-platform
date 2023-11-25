using Cloudea.Infrastructure.Database;
using Cloudea.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Service.Auth.Domain.Entities;

[AutoGenerateTable]
public record Auth_RolePermission : BaseDomainEntity
{
    /// <summary>
    /// 
    /// </summary>
    public int RoleId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int PermissionId { get; set; }
}
