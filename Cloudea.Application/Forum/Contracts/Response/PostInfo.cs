using Cloudea.Domain.Common.Shared;
using Cloudea.Domain.Forum.Entities;
using Cloudea.Domain.Identity.Entities;

namespace Cloudea.Application.Forum.Contracts.Response;

public record PostInfo
{
    private PostInfo() { }

    public Guid PostId { get; set; }
    public UserProfile Creator { get; set; }
    public Guid CreatorId { get; set; }

    public string Title { get; set; }
    public string Content { get; set; }
    public long ClickCount { get; set; }
    public long LikeCount { get; set; }
    public long DislikeCount { get; set; }
    public long FavoriteCount { get; set; }
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
            FavoriteCount = forumPost.FavoriteCount,
            ReplyCount = forumPost.ReplyCount,
            CreateTime = forumPost.CreatedOnUtc,
            LastUpdatedTime = forumPost.LastEditTime
        };
    }

    public PostInfo SetCreator(UserProfile creator)
    {
        Creator = creator;
        return this;
    }
}
