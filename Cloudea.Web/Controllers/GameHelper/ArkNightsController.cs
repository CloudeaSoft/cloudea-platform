using Cloudea.GameHelper;
using Cloudea.Infrastructure.Models;
using Cloudea.Service.GameHelper;
using Cloudea.Web.Utils.ApiBase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cloudea.Web.Controllers.GameHelper
{

    /// <summary>
    /// 明日方舟相关工具接口集
    /// </summary>
    public class ArkNightsController : NamespaceRouteControllerBase
    {
        private readonly ArkNightsService arkNights;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arkNights"></param>
        public ArkNightsController(ArkNightsService arkNights)
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
            var res = await arkNights.GetGacha(token, channelId);
            if(res.Status is false) {
                return NotFound(res);
            }
            return Ok(res);
        }
    }
}
