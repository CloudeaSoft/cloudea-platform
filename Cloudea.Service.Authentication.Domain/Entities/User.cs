using Cloudea.Infrastructure.Database;
using Cloudea.Infrastructure.Primitives;
using Cloudea.Infrastructure.Shared;
using Cloudea.Infrastructure.Utils;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Cloudea.Service.Auth.Domain.Entities;

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

    public static User Create(
        string userName,
        string nickName,
        string email,
        string password,
        string salt,
        bool enable)
    {
        return new User(
            Guid.NewGuid(),
            userName,
            nickName,
            email,
            HashPassword(password, salt),
            salt,
            enable);
    }

    public void Update()
    {

    }

    public Result SetPassword(string newPassword)
    {
        if (string.IsNullOrEmpty(newPassword)) {
            return new Error("密码不能为空");
        }
        if (newPassword.Length < 6) {
            return new Error("密码不能小于6位");
        }
        Salt = Guid.NewGuid().ToString("N");
        PasswordHash = HashPassword(newPassword, Salt);
        return Result.Success();
    }

    /// <summary>
    /// 检查密码是否正确
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    public bool CheckPassword(string password)
    {
        if (HashPassword(password, Salt) != PasswordHash) {
            return false;
        }
        return true;
    }

    private static string HashPassword(string password, string salt)
    {
        return EncryptionUtils.EncryptMD5("Cloudea" + password + "system" + salt);
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
