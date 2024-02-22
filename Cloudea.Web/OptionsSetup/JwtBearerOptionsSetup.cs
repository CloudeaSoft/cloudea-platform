using Cloudea.Service.Base.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Cloudea.Web.OptionsSetup {
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    public class JwtBearerOptionsSetup : IConfigureOptions<JwtBearerOptions>
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    {
        private readonly JwtOptions _jwtOptions;

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public JwtBearerOptionsSetup(IOptions<JwtOptions> jwtOptions)
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
        {
            _jwtOptions = jwtOptions.Value;
        }

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public void Configure(JwtBearerOptions options)
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
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
