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
        return new GetPostInfoResponse {
            PostInfo = postInfo,
            ReplyInfos = replyInfos
        };
    }

    public void SetReplyInfos(PageResponse<ReplyInfo> infos)
    {
        ReplyInfos = infos;
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

    public DateTimeOffset CreateTime { get; set; }
    public DateTimeOffset? LastUpdatedTime { get; set; }

    public static PostInfo Create(ForumPost forumPost)
    {
        return new PostInfo() {
            PostId = forumPost.Id,
            CreatorId = forumPost.OwnerUserId,
            Title = forumPost.Title,
            Content = forumPost.Content,
            ClickCount = forumPost.ClickCount,
            LikeCount = forumPost.LikeCount,
            DislikeCount = forumPost.DislikeCount,
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

    public DateTimeOffset CreateTime { get; set; }

    public List<CommentInfo> CommentInfos { get; set; } = [];

    public static ReplyInfo Create(ForumReply forumReply, List<CommentInfo> CommentInfos)
    {
        return new ReplyInfo() {
            ReplyId = forumReply.Id,
            CreatorId = forumReply.OwnerUserId,
            Title = forumReply.Title,
            Content = forumReply.Content,
            LikeCount = forumReply.LikeCount,
            DislikeCount = forumReply.DislikeCount,
            CreateTime = forumReply.CreatedOnUtc,
            CommentInfos = CommentInfos
        };
    }
}

public record CommentInfo
{
    private CommentInfo() { }

    public Guid CommentId { get; set; }
    public string Creator { set; get; } = default!;
    public Guid CreatorId { set; get; }

    public string Content { get; set; }
    public long LikeCount { get; set; }
    public long DislikeCount { get; set; }

    public DateTimeOffset CreateTime { get; set; }

    public static CommentInfo Create(ForumComment forumComment)
    {
        return new CommentInfo() {
            CommentId = forumComment.Id,
            CreatorId = forumComment.OwnerUserId,
            Content = forumComment.Content,
            LikeCount = forumComment.LikeCount,
            DislikeCount = forumComment.DislikeCount,
            CreateTime = forumComment.CreatedOnUtc
        };
    }
}