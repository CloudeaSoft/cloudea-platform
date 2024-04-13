using Cloudea.Domain.Common.Primitives;
using Cloudea.Domain.Forum.DomainEvents;

namespace Cloudea.Domain.Forum.Entities;

/// <summary>
/// 论坛回复帖
/// </summary>
public sealed class ForumReply : AggregateRoot, IAuditableEntity
{
    private ForumReply(
        Guid id,
        Guid userId,
        Guid postId,
        string content) : base(id)
    {
        ParentPostId = postId;
        OwnerUserId = userId;
        Content = content;
        LikeCount = 0;
        DislikeCount = 0;
        CommentCount = 0;
    }

    private ForumReply() { }

    public Guid ParentPostId { get; private set; }
    public Guid OwnerUserId { get; private set; }

    public string? Title { get; private set; } = string.Empty;
    public string Content { get; private set; } = string.Empty;
    public long LikeCount { get; private set; }
    public long DislikeCount { get; private set; }

    public long CommentCount { get; private set; }

    public DateTimeOffset CreatedOnUtc { get; set; }
    public DateTimeOffset? ModifiedOnUtc { get; set; }

    internal static ForumReply? Create(Guid userId, ForumPost post, string content)
    {
        if (userId == Guid.Empty) return null;
        if (post is null || post.Id == Guid.Empty) return null;
        if (string.IsNullOrEmpty(content))
        {
            return null;
        }

        var replyId = Guid.NewGuid();
        return new ForumReply(replyId, userId, post.Id, content);
    }


    public void Update(string? content)
    {
        if (!string.IsNullOrEmpty(content))
        {
            Content = content;
        }
    }

    public ForumComment? AddComment(Guid ownerUserId, string content, Guid? targetUserId)
    {
        var comment = ForumComment.Create(this, ownerUserId, targetUserId, content);
        if (comment is null)
        {
            return null;
        }

        var eventId = Guid.NewGuid();
        RaiseDomainEvent(new CommentCreatedDomainEvent(eventId, comment.Id, Id));

        return comment;
    }

    public void IncreaseCommentCount(int num = 1)
    {
        if (num < 0)
        {
            return;
        }

        CommentCount += num;
    }

    public void DecreaseCommentCount(int num = 1)
    {
        if (num < 0)
        {
            return;
        }

        CommentCount -= num;

        if (CommentCount < 0)
        {
            CommentCount = 0;
        }
    }
}
