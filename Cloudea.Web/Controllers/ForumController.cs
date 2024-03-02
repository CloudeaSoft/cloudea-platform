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
using static FreeSql.Internal.GlobalFilter;


namespace Cloudea.Web.Controllers
{
    [Authorize]
    public class ForumController : ApiControllerBase
    {
        private readonly ForumService _forumService;
        private readonly ICurrentUser _currentUser;

        public ForumController(ICurrentUser currentUser, ForumService forumService)
        {
            _currentUser = currentUser;
            _forumService = forumService;
        }

        /// <summary>
        /// Creates a ForumSection
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A newly created ForumSection</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Forum/Section
        ///     {
        ///        "SectionName": "Section #1",
        ///        "MasterId": "00000000-0000-0000-0000-00000000",
        ///        "Statement": "Section statements."
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Section([FromBody] CreateSectionRequest request, CancellationToken cancellationToken)
        {
            var res = await _forumService.PostSectionAsync(request, cancellationToken);
            if (res.IsFailure) {
                return HandleFailure(res);
            }
            return CreatedAtAction(nameof(Section), new { id = res.Data }, res.Data);
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
        public async Task<IActionResult> Post([FromBody] CreateTopicRequest request, CancellationToken cancellationToken)
        {
            var userId = await _currentUser.GetUserIdAsync();
            var res = await _forumService.PostPostAsync(userId, request, cancellationToken);

            if (res.IsFailure) {
                return HandleFailure(res);
            }

            return CreatedAtAction(
                nameof(Post),
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
        public async Task<IActionResult> Post([FromBody] string test)
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
        public async Task<IActionResult> Post(Guid id, CancellationToken cancellationToken)
        {
            var res = await _forumService.GetPostAsync(id, cancellationToken);

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
        public async Task<IActionResult> Post(
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

        /// <summary>
        /// 获取主题帖一页内容
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageIndex"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet(ID)]
        public async Task<IActionResult> PostDetail(
            Guid id, int pageIndex, CancellationToken cancellationToken)
        {
            var res = await _forumService.GetPostDetailAsync(id, pageIndex, cancellationToken);

            return res.IsSuccess ? Ok(res) : NotFound(res.Error);
        }

        /// <summary>
        /// 创建回复
        /// </summary>
        /// <param name="id"></param>
        /// <param name="content"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Reply(Guid id, string content, CancellationToken cancellationToken)
        {
            var res = await _forumService.PostReplyAsync(id, content, cancellationToken);

            if (res.IsFailure) {
                return HandleFailure(res);
            }

            return Ok(res);
        }

        /// <summary>
        /// 创建评论
        /// </summary>
        /// <param name="id"></param>
        /// <param name="content"></param>
        /// <param name="targetUserId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Comment(Guid id, string content, Guid? targetUserId, CancellationToken cancellationToken)
        {
            var res = await _forumService.PostCommentAsync(id, targetUserId, content, cancellationToken);

            if (res.IsFailure) {
                return HandleFailure(res);
            }

            return Ok(res);
        }
    }
}