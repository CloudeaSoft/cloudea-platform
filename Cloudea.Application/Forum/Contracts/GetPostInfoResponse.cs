using Cloudea.Domain.Common.Shared;
using Cloudea.Domain.Forum.Entities;

namespace Cloudea.Application.Forum.Contracts;

public class GetPostInfoResponse
{
    private GetPostInfoResponse() { }

    public PostInfo PostInfo { get; set; } = default!;

    public PageResponse<ReplyInfo>? ReplyInfos { get; set; }

    public static GetPostInfoResponse Create(PostInfo postInfo, PageResponse<ReplyInfo>? replyInfos = null)
    {
        return new GetPostInfoResponse
        {
            PostInfo = postInfo,
            ReplyInfos = replyInfos ?? new()
        };
    }
}

public record PostInfo
{
    private PostInfo() { }

    public Guid PostId { get; set; }
    public string Creator { get; set; } = default!;
    public Guid CreatorId { get; set; }

    public string Title { get; set; }
    public string Content { get; set; }
    public long ClickCount { get; set; }
    public long LikeCount { get; set; }
    public long DislikeCount { get; set; }
    public long ReplyCount { get; set; }

    public DateTimeOffset CreateTime { get; set; }
    public DateTimeOffset? LastUpdatedTime { get; set; }

    public static PostInfo Create(ForumPost forumPost)
    {
        return new PostInfo()
        {
            PostId = forumPost.Id,
            CreatorId = forumPost.OwnerUserId,
            Title = forumPost.Title,
            Content = forumPost.Content,
            ClickCount = forumPost.ClickCount,
            LikeCount = forumPost.LikeCount,
            DislikeCount = forumPost.DislikeCount,
            ReplyCount = forumPost.ReplyCount,
            CreateTime = forumPost.CreatedOnUtc,
            LastUpdatedTime = forumPost.LastEditTime
        };
    }
}

public record ReplyInfo
{
    private ReplyInfo() { }

    public Guid ReplyId { get; set; }
    public string Creator { set; get; } = default!;
    public Guid CreatorId { set; get; }

    public string? Title { get; set; }
    public string Content { get; set; }
    public long LikeCount { get; set; }
    public long DislikeCount { get; set; }
    public long CommentCount { get; set; }

    public DateTimeOffset CreateTime { get; set; }



    public static ReplyInfo Create(ForumReply forumReply)
    {
        return new ReplyInfo()
        {
            ReplyId = forumReply.Id,
            CreatorId = forumReply.OwnerUserId,
            Title = forumReply.Title,
            Content = forumReply.Content,
            LikeCount = forumReply.LikeCount,
            DislikeCount = forumReply.DislikeCount,
            CreateTime = forumReply.CreatedOnUtc,
            CommentCount = forumReply.CommentCount
        };
    }
}