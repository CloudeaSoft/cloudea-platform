using Cloudea.Infrastructure.API;
using Cloudea.Service.Auth.Domain.Abstractions;
using Cloudea.Service.Forum.Domain;
using Cloudea.Service.Forum.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Cloudea.Web.Controllers
{
    [Authorize]
    public class ForumController : ApiControllerBase
    {
        private readonly ForumDomainService _forumDomainService;
        private readonly ICurrentUser _currentUser;

        public ForumController(ForumDomainService forumDomainService, ICurrentUser currentUser)
        {
            _forumDomainService = forumDomainService;
            _currentUser = currentUser;
        }

        [HttpGet]
        public async Task<IActionResult> SectionList()
        {
            var res = await _forumDomainService.ListSectionAsync();
            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> Section()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Section(string name)
        {
            var sectionName = name;
            var masterId = await _currentUser.GetUserIdAsync();
            var res = await _forumDomainService.CreateSectionAsync(sectionName, masterId);
            return Ok(res);
        }

        /// <summary>
        /// 获取主题帖内容
        /// 包括主题帖与其回复贴
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> GetTopic()
        {
            var res = await _forumDomainService.GetTopicAsync(Guid.NewGuid());
            return Ok(res);
        }

        /// <summary>
        /// 获取主题帖列表
        /// </summary>
        [HttpGet]
        public void ListTopic()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取指定主题下的主题帖列表
        /// </summary>
        [HttpGet]
        public void ListTopicWithSection()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取指定主题下的一页主题帖列表
        /// </summary>
        [HttpGet]
        public void ListTopicWithSectionPage()
        {

            throw new NotImplementedException();
        }

        /// <summary>
        /// 发表主题帖
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostTopic([FromBody] PostTopicRequest request)
        {
            var userId = await _currentUser.GetUserIdAsync();
            var sectionId = request.sectionId;
            string title = request.title;
            string content = request.content;

            var res = await _forumDomainService.PostTopicAsync(userId, sectionId, title, content);
            return Ok(res);
        }

        /// <summary>
        /// 删除主题帖
        /// </summary>
        [HttpDelete]
        public void DeleteTopic()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取指定主题帖的一页回复
        /// </summary>
        [HttpGet]
        public void ListReplyWithTopicPage()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 发表回复帖
        /// </summary>
        [HttpPost]
        public void PostReply()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除回复贴
        /// </summary>
        [HttpDelete]
        public void DeleteReply()
        {
            throw new NotImplementedException();
        }
    }
}
