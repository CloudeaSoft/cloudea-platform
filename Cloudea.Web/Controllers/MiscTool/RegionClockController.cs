using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cloudea.MiscTool;
using Cloudea.Infrastructure.Models;
using Cloudea.Web.Utils.ApiBase;

namespace Cloudea.Web.Controllers.MiscTool
{
    /// <summary>
    /// 时间相关工具接口集
    /// </summary>
    public class RegionClockController : ApiControllerBase {

        private readonly RegionClock regionClock;

        public RegionClockController(RegionClock regionClock) {
            this.regionClock = regionClock;
        }

        [HttpGet]
        public DateTime Index() {
            return regionClock.getDate();
        }

        [HttpGet]
        public Result getTimeToUnix(DateTime dateTime) {
            return regionClock.getTimeToUnix(dateTime);
        }

        [HttpGet]
        public Result getUnixToTime(string timeStamp) {
            return regionClock.getUnixToTime(timeStamp);
        }
    }
}
