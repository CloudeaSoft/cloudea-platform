using Cloudea.Infrastructure.Primitives;
using MediatR;

namespace Cloudea.Application.Abstractions.Messaging;

public interface IDomainEventHandler<TEvent> :INotificationHandler<TEvent>
    where TEvent : IDomainEvent
{
}
