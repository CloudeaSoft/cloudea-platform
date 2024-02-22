using Cloudea.Infrastructure.Database;
using Cloudea.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Infrastructure.Primitives
{
    public abstract class AggregateRoot
    {
        private readonly List<IDomainEvent> _domainEvents = [];

        protected AggregateRoot(Guid id)
        {

        }

        protected void RaiseDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}
