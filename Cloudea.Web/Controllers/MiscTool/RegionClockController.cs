using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cloudea.MiscTool;
using Cloudea.Web.Controllers.Base;
using Cloudea.Infrastructure.Models;

namespace Cloudea.Web.Controllers.MiscTool
{

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
