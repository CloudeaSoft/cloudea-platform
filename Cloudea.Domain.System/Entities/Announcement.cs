using Cloudea.Domain.Common.Primitives;
using Cloudea.Domain.System.DomainEvents;
using Cloudea.Domain.System.ValueObjects;

namespace Cloudea.Domain.System.Entities;

public class Announcement : AggregateRoot, IAuditableEntity
{
    private Announcement(
        Guid id,
        string title,
        string content,
        Guid creatorId) : base(id)
    {
        Title = title;
        Content = content;
        CreatorId = creatorId;
    }

    private Announcement() { }

    public string Title { get; set; }

    public string Content { get; set; }

    public Guid CreatorId { get; set; }

    public ICollection<AnnouncementTranslation> Translations { get; } = [];

    public DateTimeOffset CreatedOnUtc { get; private set; }

    public DateTimeOffset? ModifiedOnUtc { get; private set; }

    public static Announcement Create(string title, string content, Guid creatorId)
    {
        Announcement announcement = new(Guid.NewGuid(), title, content, creatorId);

        announcement.RaiseDomainEvent(
            new AnnouncementCreatedDomainEvent(
                Guid.NewGuid(), announcement.Id));

        return announcement;
    }

    public AnnouncementTranslation? AddTranslation(
        LanguageCode code,
        string title,
        string content,
        Guid creatorId)
    {
        var translation = AnnouncementTranslation.Create(code, title, content, creatorId, this);
        if (translation is null)
        {
            return translation;
        }

        return translation;
    }
}
