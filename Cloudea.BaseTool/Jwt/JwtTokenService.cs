using Cloudea.Infrastructure.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Cloudea.Service.Base.Jwt;

public class JwtTokenService
{
    private readonly JwtOptions _jwtOptions;

    public JwtTokenService(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    /// <summary>
    /// Token过期时间
    /// </summary>
    const int DEFAULT_EXPIRE_DAYS = 30;

    /// <summary>
    /// 签发JWT
    /// </summary>
    /// <param name="claims">身份信息</param>
    /// <param name="expireDays">过期时间 默认:30天</param>
    /// <returns></returns>
    public Result<string> Generate(List<Claim> claims, int expireDays = DEFAULT_EXPIRE_DAYS)
    {
        if (claims == null || claims.Count == 0) {
            return Result.Fail("身份信息不能为空");
        }

        // 使用UTF8编码存储密钥
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));
        // 选择密钥与加密方式
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        // 保存Jwt信息
        var jwtToken = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims, //身份信息
            null,
            expires: DateTime.Now.AddDays(expireDays), // 过期时间
            signingCredentials: credentials // 本地签名
            );
        // 转化为JwtToken字符串
        var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        return Result.Success(token);
    }
}
