using Cloudea.Domain.Common.Primitives;
using Cloudea.Domain.System.Primitives;
using Cloudea.Domain.System.ValueObjects;
using System.Text.Json.Serialization;

namespace Cloudea.Domain.System.Entities;

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