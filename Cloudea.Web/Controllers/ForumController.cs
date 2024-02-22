using Cloudea.Entity.Forum;
using Cloudea.Infrastructure.API;
using Cloudea.Infrastructure.Models;
using Cloudea.Service.Auth.Domain.Abstractions;
using Cloudea.Service.Forum.Domain;
using Cloudea.Service.Forum.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Cloudea.Web.Controllers {
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    public class ForumController : ApiControllerBase {
        private readonly ForumApplicationService _forumService;
        private readonly ICurrentUser _currentUser;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="forumService"></param>
        public ForumController(ICurrentUser currentUser, ForumApplicationService forumService) {
            _currentUser = currentUser;
            _forumService = forumService;
        }

        /// <summary>
        /// 新增Section
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Section([FromBody] PostSectionRequest request) {
            var res = await _forumService.PostSectionAsync(request);
            return Ok(res);
        }

        /// <summary>
        /// 修改Section
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPut]
        public async Task<IActionResult> Section([FromBody] List<UpdateSectionRequest> request) {
            var res = await _forumService.UpdateSectionAsync(request);
            return Ok(res);
        }

        /// <summary>
        /// 获取Section信息
        /// </summary>
        /// <returns></returns>
        [HttpGet(ID)]
        public async Task<Result<Forum_Section>> Section(Guid id) => await _forumService.GetSectionAsync(id);

        /// <summary>
        /// 获取Section列表
        /// </summary>
        /// <returns></returns>
        [HttpGet(PageRequest)]
        public async Task<IActionResult> Section(int page, int limit) {
            var request = new PageRequest {
                Limit = limit,
                Page = page
            };
            var res = await _forumService.ListSectionAsync(request);
            return Ok(res);
        }

        /// <summary>
        /// 发表主题帖
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Topic([FromBody] PostTopicRequest request) {
            request.userId = await _currentUser.GetUserIdAsync();
            var res = await _forumService.PostTopicAsync(request);
            return Ok(res);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPatch]
        public async Task<IActionResult> Topic([FromBody] string test) {
            throw new NotImplementedException();

            return Ok();
        }

        /// <summary>
        /// 获取主题帖内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet(ID)]
        public async Task<Result<Forum_Topic>> Topic(Guid id) => await _forumService.GetTopicAsync(id);

        /// <summary>
        /// 获取主题帖列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet(PageRequest)]
        public async Task<Result<PageResponse<Forum_Topic>>> Topic(int page, int limit) {
            var request = new PageRequest {
                Limit = limit,
                Page = page
            };
            return await _forumService.ListTopicAsync(request);
        }
    }
}