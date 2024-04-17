namespace Cloudea.Domain.Identity.Enums;

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
    /// 通过邮箱重置密码
    /// </summary>
    ResetPasswordByEmail = 2,
    /// <summary>
    /// 检验手机号有效
    /// </summary>
    CheckMobilePhoneValid = 3
}
