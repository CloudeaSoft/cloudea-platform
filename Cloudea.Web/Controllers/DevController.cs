using Cloudea.Application.System;
using Cloudea.Domain.Common;
using Cloudea.Domain.Common.API;
using Cloudea.Domain.Common.Shared;
using Cloudea.Domain.Common.Utils;
using Cloudea.Domain.Identity.Attributes;
using Cloudea.Domain.System.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cloudea.Web.Controllers
{
    [Authorize]
    public class DevController : ApiControllerBase
    {
        private readonly AnnouncementService _announceService;

        public DevController(AnnouncementService announceService)
        {
            _announceService = announceService;
        }

        /// <summary>
        /// Create Announcement
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Announcement(string title, string content)
        {
            var res = await _announceService.CreateAnnouncement(title, content);
            if (res.IsFailure)
            {
                HandleFailure(res);
            }

            return Ok(res);
        }

        /// <summary>
        /// Get Announcement
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Announcement(Guid id)
        {
            var res = await _announceService.GetAnnouncement(id);

            return res.IsSuccess ? Ok(res) : NotFound(res);
        }

        /// <summary>
        /// Create Announcement Translation
        /// </summary>
        /// <param name="announcementId"></param>
        /// <param name="language"></param>
        /// <param name="region"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AnnouncementTranslation(
            Guid announcementId,
            string language,
            string region,
            string title,
            string content)
        {
            var res = await _announceService.CreateAnnouncementTranslation(
                announcementId,
                language, region, title, content);
            if (res.IsFailure)
            {
                HandleFailure(res);
            }

            return Ok(res);
        }
    }
}
