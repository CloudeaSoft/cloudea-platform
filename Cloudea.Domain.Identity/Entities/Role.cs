using Cloudea.Domain.Common.Shared;

namespace Cloudea.Domain.Identity.Entities;

/// <summary>
/// 角色
/// </summary>
public sealed class Role : Enumeration<Role>
{
    public static readonly Role User = new(1, "User");

    public static readonly Role Admin = new(2, "Admin");

    public static readonly Role SubAdmin = new(3, "SubAdmin");

    public Role(int value, string name)
        : base(value, name)
    {

    }

    public List<Permission> Permissions { get; set; } = [];
}
