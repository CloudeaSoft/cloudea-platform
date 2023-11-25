using Cloudea.Entity.Primitives;
using Cloudea.Infrastructure.Database;

namespace Cloudea.Service.Auth.Domain.Entities;

/// <summary>
/// 角色
/// </summary>
[AutoGenerateTable]
public sealed class Auth_Role : Enumeration<Auth_Role>
{
    public static readonly Auth_Role User = new(1, "User");

    public static readonly Auth_Role Admin = new(2, "Admin");

    public static readonly Auth_Role SubAdmin = new(3, "SubAdmin");

    public Auth_Role(int id, string name)
        : base(id, name)
    {

    }
}
