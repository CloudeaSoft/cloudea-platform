namespace Cloudea.Domain.Common.Primitives;

public abstract class AggregateRoot : Entity
{
    private readonly List<IDomainEvent> _domainEvents = [];

    protected AggregateRoot(Guid id)
        : base(id) { }

    protected AggregateRoot() { }

    public IReadOnlyCollection<IDomainEvent> GetDomainEvents() => _domainEvents.ToList();

    public void ClearDomainEvents() => _domainEvents.Clear();

    protected void RaiseDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

    protected void RaiseDomainEventIfAbsent(IDomainEvent eventItem)
    {
        if (!_domainEvents.Contains(eventItem)) {
            _domainEvents.Add(eventItem);
        }
    }
}
