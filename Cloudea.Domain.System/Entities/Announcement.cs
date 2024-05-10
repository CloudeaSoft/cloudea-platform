using Cloudea.Domain.Common.Primitives;

namespace Cloudea.Domain.System.Entities;

public class Announcement : AggregateRoot, IInternaltional, IAuditableEntity
{
    public Announcement(Guid id, Language language, string title, string content) : base(id)
    {
        Language = language;
        Title = title;
        Content = content;
    }

    public Announcement() { }

    public Language Language { get; set; }

    public string Title { get; set; }

    public string Content { get; set; }

    public DateTimeOffset CreatedOnUtc { get; private set; }

    public DateTimeOffset? ModifiedOnUtc { get; private set; }
}
