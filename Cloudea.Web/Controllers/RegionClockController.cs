﻿using Cloudea.Domain.Common.API;
using Cloudea.Domain.Common.Shared;
using Cloudea.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Cloudea.Web.Controllers
{
    /// <summary>
    /// 时间相关工具接口集
    /// </summary>
    public class RegionClockController : ApiControllerBase {

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
        public DateTime getTime() {
            return regionClock.GetDate();
        }

        /// <summary>
        /// 将时间转化为Unix时间戳
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        [HttpGet]
        public Result getTimeToUnix(DateTime dateTime) {
            return regionClock.GetTimeToUnix(dateTime);
        }

        /// <summary>
        /// 将Unix时间戳转化为时间
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        [HttpGet]
        public Result getUnixToTime(string timeStamp) {
            return regionClock.GetUnixToTime(timeStamp);
        }
    }
}
