namespace Cloudea.Domain.Common.Primitives;

public interface IAuditableEntity
{
    DateTimeOffset CreatedOnUtc { get; }

    DateTimeOffset? ModifiedOnUtc { get; }
}
