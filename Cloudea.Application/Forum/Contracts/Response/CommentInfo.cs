using Cloudea.Domain.Forum.Entities;
using Cloudea.Domain.Identity.Entities;

namespace Cloudea.Application.Forum.Contracts.Response;

public class CommentInfo
{
    public CommentInfo(Guid commentId, Guid creatorId, UserProfile creator, string content, long likeCount, long dislikeCount, DateTimeOffset createTime)
    {
        CommentId = commentId;
        CreatorId = creatorId;
        Creator = creator;
        Content = content;
        LikeCount = likeCount;
        DislikeCount = dislikeCount;
        CreateTime = createTime;
    }

    public Guid CommentId { get; set; }
    public UserProfile Creator { set; get; }
    public Guid CreatorId { set; get; }

    public string Content { get; set; }
    public long LikeCount { get; set; }
    public long DislikeCount { get; set; }

    public DateTimeOffset CreateTime { get; set; }

    public static CommentInfo Create(ForumComment comment, UserProfile profile) =>
        new(
            comment.Id,
            comment.OwnerUserId,
            profile,
            comment.Content,
            comment.LikeCount,
            comment.DislikeCount,
            comment.CreatedOnUtc
        );
}