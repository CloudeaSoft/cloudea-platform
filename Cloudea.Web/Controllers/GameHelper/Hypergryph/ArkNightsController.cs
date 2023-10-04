using Cloudea.GameHelper.Hypergryph;
using Cloudea.Infrastructure.Models;
using Cloudea.Web.Utils.ApiBase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cloudea.Web.Controllers.GameHelper.Hypergryph
{

    /// <summary>
    /// 明日方舟相关工具接口集
    /// </summary>
    public class ArkNightsController : NamespaceRouteControllerBase
    {
        private readonly ArkNights arkNights;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arkNights"></param>
        public ArkNightsController(ArkNights arkNights)
        {
            this.arkNights = arkNights;
        }

        /// <summary>
        /// 返回寻访记录信息 - 单页
        /// </summary>
        /// <param name="token"></param>
        /// <param name="page"></param>
        /// <param name="channelId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> GetGachaPage(int page, string token, int channelId)
        {
            return await arkNights.GetGachaPage(page, token, channelId);
        }

        /// <summary>
        /// 返回寻访记录信息 - 全部
        /// </summary>
        /// <param name="token"></param>
        /// <param name="page"></param>
        /// <param name="channelId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<int> GetGacha(string token, int channelId)
        {
            return await arkNights.GetGacha(token, channelId);
        }
    }
}
