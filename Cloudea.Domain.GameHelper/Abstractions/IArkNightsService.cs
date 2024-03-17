using Cloudea.Domain.Common.Shared;
using Cloudea.Domain.GameHelper.Models;

namespace Cloudea.Domain.GameHelper.Abstractions;

public interface IArkNightsService
{
    Task<Result<GachaHistory>> ListGacha(string token, int channelId);
}
