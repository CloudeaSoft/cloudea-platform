using Cloudea.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Service.Forum.Domain.DomainEvents {
    public sealed record PostTopicDomainEvents(Guid id) : IDomainEvent {
    }
}
