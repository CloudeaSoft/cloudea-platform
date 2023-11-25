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
    public Task<Result<GachaHistory>> GetGacha(string token, int channelId);
}
