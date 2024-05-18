using Cloudea.Domain.Common.Primitives;
using Cloudea.Domain.System.DomainEvents;
using Cloudea.Domain.System.Primitives;
using Cloudea.Domain.System.ValueObjects;
using System.Text.Json.Serialization;

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

public class AnnouncementTranslation : Entity, IInternaltional
{
    private AnnouncementTranslation(
        Guid id,
        LanguageCode code,
        string title,
        string content,
        Guid creatorId,
        Announcement announcement) : base(id)
    {
        LanguageCode = code;
        Title = title;
        Content = content;
        CreatorId = creatorId;
        Announcement = announcement;
    }

    private AnnouncementTranslation() { }

    public LanguageCode LanguageCode { get; init; }
    public string Title { get; set; }
    public string Content { get; set; }
    public Guid CreatorId { get; set; }

    public Guid AnnouncementId { get; set; }
    [JsonIgnore]
    public Announcement Announcement { get; set; }

    internal static AnnouncementTranslation? Create(
        LanguageCode code,
        string title,
        string content,
        Guid creatorId,
        Announcement announcement)
    {
        if (string.IsNullOrWhiteSpace(title) ||
            string.IsNullOrWhiteSpace(content))
            return null;

        return new AnnouncementTranslation(
            Guid.NewGuid(),
            code,
            title,
            content,
            creatorId,
            announcement);
    }
}