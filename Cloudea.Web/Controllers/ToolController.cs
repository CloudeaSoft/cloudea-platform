using Cloudea.Application.Tool;
using Cloudea.Domain.Common.API;
using Cloudea.Domain.Common.Shared;
using Cloudea.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Cloudea.Web.Controllers
{
    /// <summary>
    /// 时间相关工具接口集
    /// </summary>
    public class ToolController : ApiControllerBase {

        private readonly RegionClockService regionClock;

        public ToolController(RegionClockService regionClock)
        {
            this.regionClock = regionClock;
        }

        /// <summary>
        /// 获取当前时间
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public DateTime GetTime() {
            return regionClock.GetDate();
        }

        /// <summary>
        /// 将时间转化为Unix时间戳
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        [HttpGet]
        public Result GetTimeToUnix(DateTime dateTime) {
            return regionClock.GetTimeToUnix(dateTime);
        }

        /// <summary>
        /// 将Unix时间戳转化为时间
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        [HttpGet]
        public Result GetUnixToTime(string timeStamp) {
            return regionClock.GetUnixToTime(timeStamp);
        }
    }
}
