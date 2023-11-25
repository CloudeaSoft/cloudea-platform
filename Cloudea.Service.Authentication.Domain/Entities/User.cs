using Cloudea.Infrastructure.Database;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Cloudea.Infrastructure.Domain;
using FreeSql.DataAnnotations;

namespace Cloudea.Service.Auth.Domain.Entities;

/// <summary>
/// 用户信息
/// </summary>
[AutoGenerateTable]
[Table(Name = "Auth_User")]
public class User : IdentityUser<Guid>, IPrimaryKey, IHasTimeProperty, ISoftDelete
{
    [Column(IsIdentity = true, IsPrimary = true)]
    public long AutoIncId { get; }

    public DateTime CreationTime { get; init; }

    public DateTime ModificationTime { get; private set; }
    /// <summary>
    /// 昵称
    /// </summary>
    [StringLength(100)]
    public string? NickName { get; set; }
    /// <summary>
    /// 密码混淆
    /// </summary>
    [StringLength(255)]
    [JsonIgnore]
    public string Salt { get; set; } = default!;
    /// <summary>
    /// 头像数据
    /// </summary>
    [StringLength(500)]
    public byte[]? Avatar { get; set; }

    public bool IsDeleted { get; private set; }

    public DateTime? DeletionTime { get; private set; }

    public void SoftDelete()
    {
        this.IsDeleted = true;
        this.DeletionTime = DateTime.Now;
    }
}
