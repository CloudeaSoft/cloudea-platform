using MediatR;

namespace Cloudea.Domain.Common.Primitives;

public interface IDomainEvent : INotification
{
    public Guid Id { get; init; }
}
