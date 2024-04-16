using Cloudea.Application.Abstractions.Messaging;
using Cloudea.Domain.Forum.DomainEvents;

namespace Cloudea.Application.Forum.Events
{
    public class ReplyDeletedDomainEventHandler
        : IDomainEventHandler<ReplyDeletedDomainEvent>
    {
        public Task Handle(ReplyDeletedDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
