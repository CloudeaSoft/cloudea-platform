using Cloudea.Infrastructure.Database;
using Cloudea.Infrastructure.Primitives;
using System.ComponentModel.DataAnnotations;

namespace Cloudea.Service.Auth.Domain.Entities;

/// <summary>
/// 验证码
/// </summary>
public class VerificationCode : BaseDataEntity, IAuditableEntity
{
    private VerificationCode(
        string email,
        string code,
        VerificationCodeType codeType)
    {
        Email = email;
        VerCode = code;
        VerCodeType = codeType;
    }

    private VerificationCode()
    {

    }

    /// <summary>
    /// 邮箱
    /// </summary>
    [StringLength(50)]
    public string Email { get; set; }

    /// <summary>
    /// 验证码
    /// </summary>
    [StringLength(50)]
    public string VerCode { get; set; }

    /// <summary>
    /// 验证码过期时间
    /// </summary>
    public DateTime VerCodeValidTime { get; private set; }
    /// <summary>
    /// 验证码类型
    /// </summary>
    public VerificationCodeType VerCodeType { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public DateTime? ModifiedOnUtc { get; set; }

    public static VerificationCode? Create(
        [EmailAddress] string email,
        [Length(6, 6)] string code,
        VerificationCodeType codeType,
        int expireTime = 5)
    {
        if (string.IsNullOrWhiteSpace(email))
            return null;

        var codeEntity = new VerificationCode(email, code, codeType);
        codeEntity.SetValidTime(expireTime);
        return codeEntity;
    }

    public void Update(string code, VerificationCodeType codeType)
    {
        VerCode = code;
        VerCodeType = codeType;
    }

    public void SetValidTime(int expireTime = 5)
    {
        VerCodeValidTime = DateTime.UtcNow.AddMinutes(expireTime);
    }

    public void Valid()
    {
        VerCodeValidTime = DateTime.UtcNow;
    }
}


/// <summary>
/// 账号 验证码类型
/// </summary>
public enum VerificationCodeType
{
    /// <summary>
    /// 通过邮箱登录
    /// </summary>
    LoginByEmail = 0,
    /// <summary>
    /// 通过邮箱注册
    /// </summary>
    RegisterByEmail = 1,
    /// <summary>
    /// 通过手机重置密码
    /// </summary>
    ResetPasswordByEmail = 2,
    /// <summary>
    /// 检验手机号有效
    /// </summary>
    CheckMobilePhoneValid = 3
}
