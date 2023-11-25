using Microsoft.AspNetCore.Mvc;
using Cloudea.Infrastructure.Models;
using Cloudea.Service.Base;
using Cloudea.Infrastructure.API;

namespace Cloudea.Web.Controllers.Base
{
    /// <summary>
    /// 时间相关工具接口集
    /// </summary>
    public class RegionClockController : ApiControllerBase
    {

        private readonly RegionClockService regionClock;

        public RegionClockController(RegionClockService regionClock)
        {
            this.regionClock = regionClock;
        }

        /// <summary>
        /// 获取当前时间
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public DateTime getTime()
        {
            return regionClock.getDate();
        }

        /// <summary>
        /// 将时间转化为Unix时间戳
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        [HttpGet]
        public Result getTimeToUnix(DateTime dateTime)
        {
            return regionClock.getTimeToUnix(dateTime);
        }

        /// <summary>
        /// 将Unix时间戳转化为时间
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        [HttpGet]
        public Result getUnixToTime(string timeStamp)
        {
            return regionClock.getUnixToTime(timeStamp);
        }
    }
}
