namespace Cloudea.Infrastructure.Primitives;

public interface IAuditableEntity
{
    DateTime CreatedOnUtc { get; }

    DateTime? ModifiedOnUtc { get; }
}
