using Cloudea.GameHelper.Models.ArkNights;
using Cloudea.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Service.GameHelper.Abstractions;

public interface IArkNightsService
{
    Task<Result<GachaHistory>> ListGacha(string token, int channelId);
}
