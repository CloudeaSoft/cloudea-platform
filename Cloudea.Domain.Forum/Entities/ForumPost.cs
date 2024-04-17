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
        LikeCount = 0;
        DislikeCount = 0;
        FavoriteCount = 0;
        ReplyCount = 0;
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
    public long FavoriteCount { get; private set; }
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

    public void DeleteReply(Guid replyId)
    {
        var eventId = Guid.NewGuid();
        RaiseDomainEvent(new ReplyDeletedDomainEvent(eventId, replyId, Id));
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
        var like = ForumPostUserLike.Create(this, userId);
        if (like is null)
        {
            return null;
        }

        var eventId = Guid.NewGuid();
        RaiseDomainEvent(new PostLikeCreatedDomainEvent(eventId, like.Id, Id));

        return like;
    }

    public void DeleteLike(Guid likeId)
    {
        var eventId = Guid.NewGuid();
        RaiseDomainEvent(new PostLikeDeletedDomainEvent(eventId, likeId, Id));
    }

    public ForumPostUserLike? CreateDislike(Guid userId)
    {
        var dislike = ForumPostUserLike.Create(this, userId, false);
        if (dislike is null)
        {
            return null;
        }

        var eventId = Guid.NewGuid();
        RaiseDomainEvent(new PostDislikeCreatedDomainEvent(eventId, dislike.Id, Id));

        return dislike;
    }

    public void DeleteDislike(Guid dislikeId)
    {
        var eventId = Guid.NewGuid();
        RaiseDomainEvent(new PostDislikeDeletedDomainEvent(eventId, dislikeId, Id));
    }

    public ForumPostUserFavorite? CreateFavorite(Guid userId)
    {
        var favorite = ForumPostUserFavorite.Create(this, userId);
        if (favorite is null)
        {
            return null;
        }

        var eventId = Guid.NewGuid();
        RaiseDomainEvent(new PostFavoriteCreatedDomainEvent(Id, favorite.Id, Id));

        return favorite;
    }

    public void DeleteFavorite(Guid favoriteId)
    {
        var eventId = Guid.NewGuid();
        RaiseDomainEvent(new PostFavoriteDeletedDomainEvent(eventId, favoriteId, Id));
    }

    public void IncreaseReplyCount(int num = 1)
    {
        if (num < 0)
        {
            throw new InvalidOperationException();
        }

        ReplyCount += num;
    }

    public void DecreaseReplyCount(int num = 1)
    {
        if (num < 0)
        {
            throw new InvalidOperationException();
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
            throw new InvalidOperationException();
        }

        ClickCount += num;
    }

    public void IncreaseLikeCount(int num = 1)
    {
        if (num < 0)
        {
            throw new InvalidOperationException();
        }

        LikeCount += num;
    }

    public void DecreaseLikeCount(int num = 1)
    {
        if (num < 0)
        {
            throw new InvalidOperationException();
        }

        LikeCount -= num;

        if (LikeCount < 0)
        {
            LikeCount = 0;
        }
    }

    public void IncreaseDislikeCount(int num = 1)
    {
        if (num < 0)
        {
            throw new InvalidOperationException();
        }

        DislikeCount += num;
    }

    public void DecreaseDislikeCount(int num = 1)
    {
        if (num < 0)
        {
            throw new InvalidOperationException();
        }

        DislikeCount -= num;

        if (DislikeCount < 0)
        {
            DislikeCount = 0;
        }
    }

    public void IncreaseFavoriteCount(int num = 1)
    {
        if (num < 0)
        {
            throw new InvalidOperationException();
        }

        FavoriteCount += num;
    }

    public void DecreaseFavoriteCount(int num = 1)
    {
        if (num < 0)
        {
            throw new InvalidOperationException();
        }

        FavoriteCount -= num;

        if (FavoriteCount < 0)
        {
            FavoriteCount = 0;
        }
    }
}
