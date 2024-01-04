using Cloudea.Infrastructure.Database;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Cloudea.Infrastructure.Domain;
using FreeSql.DataAnnotations;
using MySqlX.XDevAPI.Relational;

namespace Cloudea.Service.Auth.Domain.Entities;

/// <summary>
/// 用户信息
/// </summary>
[AutoGenerateTable]
[Table(Name = "auth_user")]
public record User : BaseDomainEntity, ISoftDelete
{
    /// <summary>
    /// 用户名
    /// </summary>
    [StringLength(100)]
    public string UserName { get; set; }
    /// <summary>
    /// 昵称
    /// </summary>
    [StringLength(100)]
    public string NickName { get; set; }
    /// <summary>
    /// 邮箱
    /// </summary>
    [StringLength(100)]
    [EmailAddress]
    [JsonIgnore]
    public string Email { get; set; }
    /// <summary>
    /// 密码 (加密后)
    /// </summary>
    [StringLength(255)]
    [JsonIgnore]
    public string PasswordHash { get; set; }
    /// <summary>
    /// 密码混淆
    /// </summary>
    [StringLength(255)]
    [JsonIgnore]
    public string Salt { get; set; }
    /// <summary>
    /// 头像数据
    /// </summary>
    [StringLength(500)]
    public byte[]? Avatar { get; set; }
    /// <summary>
    /// 是否启用
    /// </summary>
    [JsonIgnore]
    public bool Enable { get; set; }
    /// <summary>
    /// 软删除标记
    /// </summary>
    [JsonIgnore]
    public bool IsDeleted { get; private set; }
    /// <summary>
    /// 删除时间
    /// </summary>
    [JsonIgnore]
    public DateTime? DeletionTime { get; private set; }

    public void SoftDelete()
    {
        this.IsDeleted = true;
        this.DeletionTime = DateTime.Now;
    }

    public static User Create(string userName, string nickName, string email, string passwordHash, string salt, bool enable)
    {
        return new User() {
            UserName = userName,
            NickName = nickName,
            Email = email,
            PasswordHash = passwordHash,
            Salt = salt,
            Enable = enable,
        };
    }
}
