using Cloudea.Infrastructure.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Cloudea.Web.OptionsSetup;

public class JwtBearerOptionsSetup : IConfigureOptions<JwtBearerOptions>
{
    private readonly JwtOptions _jwtOptions;

    public JwtBearerOptionsSetup(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public void Configure(JwtBearerOptions options)
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;

        options.TokenValidationParameters = new() {
            ValidIssuer = _jwtOptions.Issuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret)),
            ValidAudience = _jwtOptions.Audience,
            ValidateLifetime = true
        };
    }
}
