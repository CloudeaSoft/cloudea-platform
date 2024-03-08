using Cloudea.Domain.Common.Shared;
using Cloudea.Domain.Identity.ValueObjects;
using Cloudea.Infrastructure.Database;
using Cloudea.Infrastructure.Primitives;
using Cloudea.Infrastructure.Shared;
using Cloudea.Infrastructure.Utils;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Cloudea.Domain.Identity.Entities;

/// <summary>
/// 用户信息
/// </summary>
public class User : Entity, ISoftDelete
{
    private User(
        Guid id,
        string userName,
        string nickName,
        string email,
        string passwordHash,
        string salt,
        bool enable) : base(id)
    {
        UserName = userName;
        NickName = nickName;
        Email = email;
        PasswordHash = passwordHash;
        Salt = salt;
        Enable = enable;
    }

    private User() { }

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }
    /// <summary>
    /// 昵称
    /// </summary>
    public string NickName { get; set; }
    /// <summary>
    /// 邮箱
    /// </summary>
    [EmailAddress]
    [JsonIgnore]
    public string Email { get; set; }
    /// <summary>
    /// 密码 (加密后)
    /// </summary>
    [JsonIgnore]
    public string PasswordHash { get; private set; }
    /// <summary>
    /// 密码混淆
    /// </summary>
    [JsonIgnore]
    public string Salt { get; private set; }
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
        IsDeleted = true;
        DeletionTime = DateTime.Now;
    }

    public static User Create(
        string userName,
        string nickName,
        string email,
        PasswordHash passwordHash,
        Salt salt,
        bool enable)
    {
        return new User(
            Guid.NewGuid(),
            userName,
            nickName,
            email,
            passwordHash.Value,
            salt.Value,
            enable);
    }

    public void Update()
    {

    }

    public void SetPassword(PasswordHash newPassword, Salt salt)
    {
        PasswordHash = newPassword.Value;
        Salt = salt.Value;
    }

    public void EnableMe()
    {
        Enable = true;
    }

    public void DisableMe()
    {
        Enable = false;
    }
}
