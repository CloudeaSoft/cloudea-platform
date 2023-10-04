using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWT
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("passport", "12345"));
            claims.Add(new Claim("QQ", "4321"));
            claims.Add(new Claim("f", "154"));
            claims.Add(new Claim("txt", "3212"));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, "3212"));
            claims.Add(new Claim(ClaimTypes.Name, "3212"));
            string key = "abcsdsfe98*()ifjsoaf@JOSI)IO";
            DateTime expire = DateTime.Now.AddHours(1);

            byte[] secBytes = Encoding.UTF8.GetBytes(key);
            var secKey = new SymmetricSecurityKey(secBytes);
            var credentials = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(claims: claims,
                expires: expire, signingCredentials: credentials);
            string jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
            Console.WriteLine(jwt);
        }
    }
}