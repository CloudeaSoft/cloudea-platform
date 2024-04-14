using Cloudea.Domain.Forum.Entities;
using Cloudea.Domain.Identity.Entities;

namespace Cloudea.Application.Forum.Contracts.Response;

public record ReplyInfo
{
    private ReplyInfo() { }

    public Guid ReplyId { get; set; }
    public UserProfile Creator { set; get; }
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