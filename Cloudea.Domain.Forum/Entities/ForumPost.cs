using Cloudea.Infrastructure.Primitives;
using Cloudea.Service.Forum.Domain.DomainEvents;

namespace Cloudea.Service.Forum.Domain.Entities;

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
        LastClickTime = DateTime.UtcNow;
        LastEditTime = DateTime.UtcNow;
    }

    private ForumPost() { }

    public Guid ParentSectionId { get; set; }
    public Guid OwnerUserId { get; set; }

    public string Title { get; set; }
    public string Content { get; set; }
    public long ClickCount { get; set; }

    public DateTime LastClickTime { get; set; }
    public DateTime LastEditTime { get; set; }

    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    public static ForumPost? Create(
        Guid userId,
        ForumSection section,
        string title,
        string content)
    {
        if (string.IsNullOrEmpty(title)) {
            return null;
        }

        if (string.IsNullOrEmpty(content)) {
            return null;
        }

        var id = Guid.NewGuid();

        var post = new ForumPost(id, userId, section.Id, title, content);

        post.RaiseDomainEvent(new PostCreatedDomainEvent(
            Guid.NewGuid(),
            post.Id,
            post.ParentSectionId));

        return post;
    }


    public void Update(
        string? title,
        string? content)
    {
        if (!string.IsNullOrEmpty(title)) {
            Title = title;
        }

        if (!string.IsNullOrEmpty(content)) {
            Content = content;
        }
    }
}
