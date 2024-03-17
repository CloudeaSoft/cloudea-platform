using Cloudea.Domain.Common.Shared;
using System.Security.Claims;

namespace Cloudea.Application.Utils
{
    public interface IJwtTokenService
    {
        Result<string> Generate(List<Claim> claims, int expireDays = 30);
    }
}