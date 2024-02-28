using Cloudea.Application.Contracts;
using Cloudea.Application.Forum;
using Cloudea.Infrastructure.API;
using Cloudea.Infrastructure.Shared;
using Cloudea.Service.Auth.Domain.Abstractions;
using Cloudea.Service.Forum.Domain.Entities;
using Cloudea.Service.Forum.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;
using System.Threading;


namespace Cloudea.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    public class ForumController : ApiControllerBase
    {
        private readonly ForumService _forumService;
        private readonly ICurrentUser _currentUser;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="forumService"></param>
        public ForumController(ICurrentUser currentUser, ForumService forumService)
        {
            _currentUser = currentUser;
            _forumService = forumService;
        }

        /// <summary>
        /// 新增Section
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Section([FromBody] CreateSectionRequest request, CancellationToken cancellationToken)
        {
            var res = await _forumService.PostSectionAsync(request, cancellationToken);
            return Ok(res);
        }

        /// <summary>
        /// 修改Section
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut(ID)]
        public async Task<IActionResult> Section(
            Guid id,
            [FromBody] UpdateSectionRequest request,
            CancellationToken cancellationToken)
        {
            var res = await _forumService.UpdateSectionAsync(id, request, cancellationToken);
            return Ok(res);
        }

        /// <summary>
        /// 获取Section信息
        /// </summary>
        /// <returns></returns>
        [HttpGet(ID)]
        public async Task<IActionResult> Section(
            Guid id,
            CancellationToken cancellationToken)
        {
            var res = await _forumService.GetSectionAsync(id, cancellationToken);
            return Ok(res);
        }

        /// <summary>
        /// 获取Section列表
        /// </summary>
        /// <returns></returns>
        [HttpGet(PageRequest)]
        public async Task<IActionResult> Section(
            int page,
            int limit,
            CancellationToken cancellationToken = default)
        {
            var request = new PageRequest {
                PageSize = limit,
                PageIndex = page
            };

            var res = await _forumService.ListSectionAsync(request, cancellationToken);

            return res.IsSuccess ? Ok(res) : NotFound(res.Error);
        }

        /// <summary>
        /// 发表主题帖
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Topic([FromBody] CreateTopicRequest request, CancellationToken cancellationToken)
        {
            var userId = await _currentUser.GetUserIdAsync();
            var res = await _forumService.PostTopicAsync(userId, request, cancellationToken);

            if (res.IsFailure) {
                return HandleFailure(res);
            }

            return CreatedAtAction(
                nameof(Topic),
                new { id = res.Data },
                res.Data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPatch]
        public async Task<IActionResult> Topic([FromBody] string test)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取主题帖内容
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet(ID)]
        public async Task<IActionResult> Topic(Guid id, CancellationToken cancellationToken)
        {
            var res = await _forumService.GetTopicAsync(id, cancellationToken);

            return res.IsSuccess ? Ok(res) : NotFound(res.Error);
        }

        /// <summary>
        /// 获取主题帖列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet(PageRequest)]
        public async Task<IActionResult> Topic(
            int page,
            int limit,
            CancellationToken cancellationToken)
        {
            var request = new PageRequest {
                PageSize = limit,
                PageIndex = page
            };

            var res = await _forumService.ListTopicAsync(request, cancellationToken);

            return res.IsSuccess ? Ok(res) : NotFound(res.Error);
        }
    }
}