using Cloudea.Application.Abstractions.Messaging;
using Cloudea.Service.Forum.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Application.Forum.Events
{
    public class ReplyCreatedDomainEventHandler
        : IDomainEventHandler<ReplyCreatedDomainEvent>
    {
        public async Task Handle(ReplyCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("未完成的Handler");
        }
    }
}
