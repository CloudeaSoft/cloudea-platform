using Cloudea.Domain.Common.Shared;
using System.Security.Claims;

namespace Cloudea.Application.Infrastructure
{
    public interface IJwtProvider
    {
        Result<string> Generate(List<Claim> claims);
    }
}