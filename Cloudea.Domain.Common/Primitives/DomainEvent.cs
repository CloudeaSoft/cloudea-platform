namespace Cloudea.Domain.Common.Primitives;

public abstract record DomainEvent(Guid Id) : IDomainEvent;
