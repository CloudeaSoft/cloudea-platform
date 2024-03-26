using Cloudea.Domain.Common.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Domain.Identity.DomainEvents
{
    public record UserCreatedDomainEvent(Guid Id, Guid UserId) : DomainEvent(Id);
}
