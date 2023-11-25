using Cloudea.Entity.Primitives;
using Cloudea.Infrastructure.Database;

namespace Cloudea.Entity.Base.Role;

/// <summary>
/// 角色
/// </summary>
[AutoGenerateTable]
public sealed class Base_Role : Enumeration<Base_Role>
{
    public static readonly Base_Role User = new(1, "User");

    public static readonly Base_Role Admin = new(2, "Admin");

    public static readonly Base_Role SubAdmin = new(3, "SubAdmin");

    public Base_Role(int id, string name)
        : base(id, name)
    {

    }
}
