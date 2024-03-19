using Cloudea.Application.Abstractions;
using Cloudea.Application.Forum;
using Cloudea.Application.Forum.Contracts;
using Cloudea.Domain.Common.API;
using Cloudea.Domain.Common.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Cloudea.Web.Controllers;

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
        var res = await _forumService.CreateSectionAsync(request, cancellationToken);
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
    public async Task<IActionResult> Post([FromBody] CreatePostRequest request, CancellationToken cancellationToken)
    {
        var userId = await _currentUser.GetUserIdAsync();
        var res = await _forumService.CreatePostAsync(userId, request, cancellationToken);

        if (res.IsFailure) {
            return HandleFailure(res);
        }

        return CreatedAtAction(
            nameof(Post),
            new { id = res.Data },
            res.Data);
    }

    /// <summary>
    /// 获取主题帖一页内容
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost(ID+"/Info")]
    public async Task<IActionResult> Post(
        Guid id,
        [FromBody] PageRequest request,
        CancellationToken cancellationToken)
    {
        var res = await _forumService.GetPostInfoAsync(id, request, cancellationToken);

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

        var res = await _forumService.ListPostAsync(request, cancellationToken);

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