using Cloudea.GameHelper.Models.ArkNights;
using Cloudea.Infrastructure.Shared;

namespace Cloudea.Service.GameHelper.Abstractions;

public interface IArkNightsService
{
    Task<Result<GachaHistory>> ListGacha(string token, int channelId);
}
