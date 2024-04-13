using Cloudea.Domain.Common.Primitives;
using Cloudea.Domain.Forum.DomainEvents;

namespace Cloudea.Domain.Forum.Entities;

/// <summary>
/// 论坛主题帖
/// </summary>
public sealed class ForumPost : AggregateRoot, IAuditableEntity
{
    private ForumPost(
        Guid id,
        Guid userId,
        Guid sectionId,
        string title,
        string content) : base(id)
    {
        ParentSectionId = sectionId;
        OwnerUserId = userId;
        Title = title;
        Content = content;
        ClickCount = 0;
        LastClickTime = DateTimeOffset.UtcNow;
        LastEditTime = DateTimeOffset.UtcNow;
    }

    private ForumPost() { }

    public Guid ParentSectionId { get; set; }
    public Guid OwnerUserId { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public long ClickCount { get; private set; }

    public long LikeCount { get; private set; }
    public long DislikeCount { get; private set; }
    public long ReplyCount { get; private set; }

    public DateTimeOffset LastClickTime { get; set; }
    public DateTimeOffset LastEditTime { get; set; }

    public DateTimeOffset CreatedOnUtc { get; set; }
    public DateTimeOffset? ModifiedOnUtc { get; set; }

    internal static ForumPost? Create(
        Guid userId,
        ForumSection section,
        string title,
        string content)
    {
        if (userId == Guid.Empty) return null;
        if (section == null || section.Id == Guid.Empty) return null;
        if (string.IsNullOrEmpty(title)) return null;
        if (string.IsNullOrEmpty(content)) return null;

        var id = Guid.NewGuid();

        return new ForumPost(id, userId, section.Id, title, content);
    }


    public void Update(
        string? title,
        string? content)
    {
        if (!string.IsNullOrEmpty(title))
        {
            Title = title;
        }

        if (!string.IsNullOrEmpty(content))
        {
            Content = content;
        }
    }

    public ForumReply? CreateReply(Guid userId, string content)
    {
        var reply = ForumReply.Create(userId, this, content);
        if (reply is null)
        {
            return null;
        }

        var evnetId = Guid.NewGuid();
        RaiseDomainEvent(new ReplyCreatedDomainEvent(evnetId, reply.Id, Id));

        return reply;
    }

    public ForumPostUserHistory? CreateHistory(Guid userId)
    {
        var history = ForumPostUserHistory.Create(this, userId);
        if (history is null)
        {
            return null;
        }

        var eventId = Guid.NewGuid();
        RaiseDomainEvent(new PostHistoryCreatedDomainEvent(eventId, history.Id, Id));

        return history;
    }

    public ForumPostUserLike? CreateLike(Guid userId)
    {
        return ForumPostUserLike.Create(this, userId);
    }

    public ForumPostUserLike? CreateDislike(Guid userId)
    {
        return ForumPostUserLike.Create(this, userId, false);
    }

    public ForumPostUserFavorite? CreateFavorite(Guid userId)
    {
        return ForumPostUserFavorite.Create(this, userId);
    }

    public void IncreaseReplyCount(int num = 1)
    {
        if (num < 0)
        {
            return;
        }

        ReplyCount += num;
    }

    public void DecreaseReplyCount(int num = 1)
    {
        if (num < 0)
        {
            return;
        }

        ReplyCount -= num;

        if (ReplyCount < 0)
        {
            ReplyCount = 0;
        }
    }

    public void IncreaseClickCount(int num = 1)
    {
        if (num < 0)
        {
            return;
        }

        ClickCount += num;
    }
}
