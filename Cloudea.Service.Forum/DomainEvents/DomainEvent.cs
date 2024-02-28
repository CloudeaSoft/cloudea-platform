using Cloudea.Infrastructure.Primitives;

namespace Cloudea.Service.Forum.Domain.DomainEvents;

public abstract record DomainEvent(Guid Id) : IDomainEvent;
