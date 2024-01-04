using Cloudea.Entity.Primitives;
using Cloudea.Infrastructure.Database;
using Cloudea.Infrastructure.Models;
using FreeSql.DataAnnotations;

namespace Cloudea.Service.Auth.Domain.Entities;

/// <summary>
/// 角色
/// </summary>
[AutoGenerateTable]
[Table(Name = "auth_role")]
public sealed class Role : Enumeration<Role>
{
    public static readonly Role User = new(1, "User");

    public static readonly Role Admin = new(2, "Admin");

    public static readonly Role SubAdmin = new(3, "SubAdmin");

    public Role(int id, string name)
        : base(id, name)
    {

    }

    [Column]
    public List<Permission> Permissions { get; set; } = [];
}
