using Cloudea.Domain.Common.Database;
using Cloudea.Domain.Common.Primitives;
using Cloudea.Domain.Identity.DomainEvents;
using Cloudea.Domain.Identity.ValueObjects;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Cloudea.Domain.Identity.Entities;

/// <summary>
/// 用户信息
/// </summary>
public class User : AggregateRoot, ISoftDelete
{
    private User(
        Guid id,
        string userName,
        string email,
        string passwordHash,
        string salt,
        bool enable) : base(id)
    {
        UserName = userName;
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
    /// 邮箱
    /// </summary>
    [EmailAddress]
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
    public DateTimeOffset? DeletionTime { get; private set; }

    public void SoftDelete()
    {
        IsDeleted = true;
        DeletionTime = DateTimeOffset.Now;
    }

    public static User Create(
        string userName,
        string email,
        PasswordHash passwordHash,
        Salt salt,
        bool enable)
    {
        var user = new User(
            Guid.NewGuid(),
            userName,
            email,
            passwordHash.Value,
            salt.Value,
            enable);

        var domainEvent = new UserCreatedDomainEvent(Guid.NewGuid(), user.Id);
        user.RaiseDomainEvent(domainEvent);

        return user;
    }

    public void SetEmail()
    {

    }

    public void SetPassword(PasswordHash newPassword, Salt salt)
    {
        PasswordHash = newPassword.Value;
        Salt = salt.Value;
    }

    public void EnableUser()
    {
        Enable = true;
    }

    public void DisableUser()
    {
        Enable = false;
    }
}
