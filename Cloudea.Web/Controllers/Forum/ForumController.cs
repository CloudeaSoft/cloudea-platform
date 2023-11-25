using Cloudea.Entity.Forum;
using Cloudea.Infrastructure.API;
using Cloudea.Service.Forum;
using Microsoft.AspNetCore.Mvc;

namespace Cloudea.Web.Controllers.Forum
{
    public class ForumController : ApiControllerBase
    {
        private readonly ForumTopicService _forumTopicService;
        private readonly ForumSectionService _forumSectionService;
        private readonly ForumReplyService _forumReplyService;

        public ForumController(ForumTopicService forumTopicService, ForumReplyService forumReplyService, ForumSectionService forumSectionService)
        {
            _forumTopicService = forumTopicService;
            _forumReplyService = forumReplyService;
            _forumSectionService = forumSectionService;
        }

        [HttpPost]
        public async Task<IActionResult> Section(Forum_Section section)
        {
            var res = await _forumSectionService.Create(section);
            return Ok(res.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Topic(Forum_Topic topic)
        {
            var res = await _forumTopicService.Create(topic);
            return Ok(res.Data);
        }
    }
}
