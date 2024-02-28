using Cloudea.Service.Base.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Cloudea.Web.OptionsSetup {
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
            options.TokenValidationParameters = new TokenValidationParameters {
                //NameClaimType = JwtClaimTypes.Name,
                //RoleClaimType = JwtClaimTypes.Role,

                // 颁发者
                ValidateIssuer = true,
                ValidIssuer = _jwtOptions.Issuer,

                // 颁发密钥
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret)),

                // 受颁发人
                ValidateAudience = true,
                ValidAudience = _jwtOptions.Audience,

                // 密钥生存周期
                ValidateLifetime = true
            };
        }
    }
}
