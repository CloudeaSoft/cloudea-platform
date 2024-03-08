using Cloudea.Infrastructure.API;
using Cloudea.Service.GameHelper.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Cloudea.Web.Controllers.GameHelper
{

    /// <summary>
    /// 明日方舟相关工具接口集
    /// </summary>
    public class ArkNightsController : ApiControllerBase
    {
        private readonly IArkNightsService arkNights;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arkNights"></param>
        public ArkNightsController(IArkNightsService arkNights)
        {
            this.arkNights = arkNights;
        }

        /// <summary>
        /// 返回寻访记录信息 - 全部
        /// </summary>
        /// <param name="token"></param>
        /// <param name="channelId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Gacha(string token, int channelId)
        {
            var res = await arkNights.ListGacha(token, channelId);
            if (res.IsFailure) {
                return NotFound(res);
            }
            return Ok(res);
        }
    }
}
