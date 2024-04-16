using Cloudea.Application.Abstractions;
using Cloudea.Application.Forum;
using Cloudea.Application.Forum.Contracts.Request;
using Cloudea.Domain.Common.API;
using Cloudea.Domain.Common.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Cloudea.Web.Controllers;

[Authorize]
[Route("api/[controller]")]
public class ForumController : ApiControllerBase
{
    private readonly ForumService _forumService;

    public ForumController(ForumService forumService)
    {
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
    [HttpPost("Section")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Section([FromBody] CreateSectionRequest request, CancellationToken cancellationToken)
    {
        var res = await _forumService.CreateSectionAsync(request, cancellationToken);
        if (res.IsFailure)
        {
            return HandleFailure(res);
        }
        return CreatedAtAction(
            nameof(Section),
            new { id = res.Data },
            res);
    }

    /// <summary>
    /// 修改Section
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("Section/" + ID)]
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
    [HttpGet("Section/" + ID)]
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
    [AllowAnonymous]
    [HttpGet("Section")]
    public async Task<IActionResult> Section(
        int page,
        int limit,
        CancellationToken cancellationToken = default)
    {
        var request = new PageRequest
        {
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
    [HttpPost("Post")]
    public async Task<IActionResult> Post([FromBody] CreatePostRequest request, CancellationToken cancellationToken)
    {
        var res = await _forumService.CreatePostAsync(request, cancellationToken: cancellationToken);

        if (res.IsFailure)
        {
            return HandleFailure(res);
        }

        return CreatedAtAction(
            nameof(Post),
            new { id = res.Data },
            res);
    }

    /// <summary>
    /// 获取主题帖
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("Post/" + ID)]
    public async Task<IActionResult> Post(
        Guid id,
        CancellationToken cancellationToken)
    {
        var res = await _forumService.GetPostAsync(id, cancellationToken);

        return res.IsSuccess ? Ok(res) : NotFound(res.Error);
    }

    /// <summary>
    /// 获取主题帖列表
    /// </summary>
    /// <param name="page"></param>
    /// <param name="limit"></param>
    /// <param name="sectionId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet("Post")]
    public async Task<IActionResult> Post(
        int page,
        int limit,
        Guid? sectionId,
        CancellationToken cancellationToken)
    {
        var request = new PageRequest
        {
            PageSize = limit,
            PageIndex = page
        };

        var res = await _forumService.ListPostAsync(request, sectionId, cancellationToken);

        return res.IsSuccess ? Ok(res) : NotFound(res);
    }

    /// <summary>
    /// 创建喜欢
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("Post/" + ID + "/Like")]
    public async Task<IActionResult> PostLike(
        Guid id,
        CancellationToken cancellationToken)
    {
        var res = await _forumService.CreateLikeOnPostAsync(id, cancellationToken);

        if (res.IsFailure)
            return HandleFailure(res);

        return Ok(res);
    }

    /// <summary>
    /// 获取喜欢/不喜欢
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("Post/" + ID + "/Like")]
    public async Task<IActionResult> PostLikeGet(
        Guid id,
        CancellationToken cancellationToken)
    {
        var res = await _forumService.GetLikeOnPostAsync(id, cancellationToken);

        return res.IsSuccess ? Ok(res) : NotFound(res);
    }

    /// <summary>
    /// 删除喜欢/不喜欢
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("Post/" + ID + "/Like")]
    public async Task<IActionResult> PostLikeDelete(
        Guid id,
        CancellationToken cancellationToken)
    {
        var res = await _forumService.DeleteLikeOnPostAsync(id, cancellationToken);

        if (res.IsFailure)
            return HandleFailure(res);

        return Ok(res);
    }

    /// <summary>
    /// 创建不喜欢
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("Post/" + ID + "/Dislike")]
    public async Task<IActionResult> PostDislike(
        Guid id,
        CancellationToken cancellationToken)
    {
        var res = await _forumService.CreateDislikeOnPostAsync(id, cancellationToken);

        if (res.IsFailure)
            return HandleFailure(res);

        return Ok(res);
    }

    /// <summary>
    /// 创建收藏
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("Post/" + ID + "/Favorite")]
    public async Task<IActionResult> PostFavorite(
        Guid id,
        CancellationToken cancellationToken)
    {
        var res = await _forumService.CreateFavoriteOnPostAsync(id, cancellationToken);

        if (res.IsFailure)
            return HandleFailure(res);

        return Ok(res);
    }

    /// <summary>
    /// 获取收藏
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("Post/" + ID + "/Favorite")]
    public async Task<IActionResult> PostFavoriteGet(
        Guid id,
        CancellationToken cancellationToken)
    {
        var res = await _forumService.GetFavoriteOnPostAsync(id, cancellationToken);

        return res.IsSuccess ? Ok(res) : NotFound(res);
    }

    /// <summary>
    /// 删除收藏
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("Post/" + ID + "/Favorite")]
    public async Task<IActionResult> PostFavoriteDelete(
        Guid id,
        CancellationToken cancellationToken)
    {
        var res = await _forumService.DeleteFavoriteOnPostAsync(id, cancellationToken);

        if (res.IsFailure)
            return HandleFailure(res);

        return Ok(res);
    }

    /// <summary>
    /// 创建回复
    /// </summary>
    /// <param name="id"></param>
    /// <param name="content"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("Reply")]
    public async Task<IActionResult> Reply(Guid id, string content, CancellationToken cancellationToken)
    {
        var res = await _forumService.PostReplyAsync(id, content, cancellationToken);

        if (res.IsFailure)
        {
            return HandleFailure(res);
        }

        return Ok(res);
    }

    /// <summary>
    /// 获取一页回复
    /// </summary>
    /// <param name="postId"></param>
    /// <param name="page"></param>
    /// <param name="limit"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("Reply")]
    public async Task<IActionResult> Reply(Guid postId, int page, int limit, CancellationToken cancellationToken)
    {
        var res = await _forumService.ListReplyAsync(postId, new PageRequest(page, limit), cancellationToken);

        if (res.IsFailure)
        {
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
    [HttpPost("Comment")]
    public async Task<IActionResult> Comment(Guid id, string content, Guid? targetUserId, CancellationToken cancellationToken)
    {
        var res = await _forumService.PostCommentAsync(id, targetUserId, content, cancellationToken);

        if (res.IsFailure)
        {
            return HandleFailure(res);
        }

        return Ok(res);
    }

    /// <summary>
    /// 获取评论
    /// </summary>
    /// <param name="id"></param>
    /// <param name="page"></param>
    /// <param name="limit"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet("Comment")]
    public async Task<IActionResult> Comment(Guid id, int page, int limit, CancellationToken cancellationToken)
    {
        var res = await _forumService.ListCommentAsync(id, new PageRequest(page, limit), cancellationToken);

        if (res.IsFailure)
        {
            return HandleFailure(res);
        }

        return Ok(res);
    }
}