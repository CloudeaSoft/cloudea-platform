using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiscTool;

namespace Cloudea.Web.Controllers.MiscTool {

    public class RegionClockController : ApiControllerBase {
        private readonly RegionClock regionClock;

        public RegionClockController(RegionClock regionClock) {
            this.regionClock = regionClock;
        }

        [HttpGet]
        public async Task<IActionResult> Index() {
            return Ok(await regionClock.getDate());
        }
    }
}
