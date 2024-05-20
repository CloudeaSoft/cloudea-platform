namespace Cloudea.Infrastructure.Jwt;

public class JwtOptions
{
    /// <summary>
    /// 密钥
    /// </summary>
    public string Secret { get; init; } = default!;
    /// <summary>
    /// 发行人
    /// </summary>
    public string Issuer { get; init; } = default!;
    /// <summary>
    /// 接受人
    /// </summary>
    public string Audience { get; init; } = default!;
}
