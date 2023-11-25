namespace Cloudea.Service.Base.Jwt;

public class JwtOptions
{
    public const string SectionName = "Jwt";
    /// <summary>
    /// 密钥
    /// </summary>
    public string Secret { get; init; }
    /// <summary>
    /// 发行人
    /// </summary>
    public string Issuer { get; init; }
    /// <summary>
    /// 接受人
    /// </summary>
    public string Audience { get; init; }
    /// <summary>
    /// 过期时间
    /// </summary>
    public int ExpireSeconds { get; init; } = 30;
}
