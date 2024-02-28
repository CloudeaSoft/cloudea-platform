using MediatR;

namespace Cloudea.Infrastructure.Primitives;

public interface IDomainEvent : INotification
{
    public Guid Id { get; init; }
}
