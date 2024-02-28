namespace Cloudea.Service.Base.Jwt;

public class JwtOptions
{
    /// <summary>
    /// 密钥
    /// </summary>
    public string Secret { get; init; } = string.Empty;
    /// <summary>
    /// 发行人
    /// </summary>
    public string Issuer { get; init; } = string.Empty;
    /// <summary>
    /// 接受人
    /// </summary>
    public string Audience { get; init; } = string.Empty;
    /// <summary>
    /// 过期时间
    /// </summary>
    public int ExpireSeconds { get; init; } = 30;
}
