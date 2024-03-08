using Cloudea.Domain.Identity.Enums;
using Cloudea.Infrastructure.Database;
using Cloudea.Infrastructure.Primitives;
using System.ComponentModel.DataAnnotations;

namespace Cloudea.Domain.Identity.Entities;

/// <summary>
/// 验证码
/// </summary>
public class VerificationCode : BaseDataEntity, IAuditableEntity
{
    private VerificationCode(
        Guid id,
        string email,
        string code,
        VerificationCodeType codeType)
    {
        Id = id;
        Email = email;
        VerCode = code;
        VerCodeType = codeType;
    }

    private VerificationCode() { }

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

        var codeEntity = new VerificationCode(Guid.NewGuid(), email, code, codeType);
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
}
