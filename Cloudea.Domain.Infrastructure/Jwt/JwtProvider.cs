using Cloudea.Application.Infrastructure;
using Cloudea.Domain.Common.Shared;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Cloudea.Infrastructure.Jwt;

public class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _jwtOptions;

    public JwtProvider(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    /// <summary>
    /// Generate JWT
    /// </summary>
    /// <param name="claims">身份信息</param>
    /// <returns></returns>
    public Result<string> Generate(List<Claim> claims)
    {
        if (claims is null || claims.Count == 0)
        {
            return new Error("JwtProvider.UnknownError");
        }

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtOptions.Secret)), 
            SecurityAlgorithms.HmacSha256);

        var jwtToken = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims,
            null,
            DateTime.UtcNow.AddDays(999),
            credentials);

        return new JwtSecurityTokenHandler()
            .WriteToken(jwtToken);
    }
}
