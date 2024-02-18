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
        private readonly ForumApplicationService _forumService;
        private readonly ICurrentUser _currentUser;


        public ForumController(ICurrentUser currentUser, ForumApplicationService forumService)
        {
            _currentUser = currentUser;
            _forumService = forumService;
        }

        /// <summary>
        /// 获取Section列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> SectionList()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取Section信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Section()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 新增Section
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Section(string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取主题帖内容
        /// 包括主题帖与其回复贴
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> GetTopic()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取主题帖列表
        /// </summary>
        [HttpGet]
        public void TopicList()
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
        public async Task<IActionResult> Topic([FromBody] PostTopicRequest request)
        {
            request.userId = await _currentUser.GetUserIdAsync();
            var res = await _forumService.PostTopicAsync(request);
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
