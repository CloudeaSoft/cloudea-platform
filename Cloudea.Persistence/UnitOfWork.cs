using Cloudea.Infrastructure.Primitives;
using Cloudea.Infrastructure.Repositories;
using Cloudea.Persistence.Outbox;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace Cloudea.Persistence;

internal sealed class UnitOfWork(ApplicationDbContext dbContext, IPublisher publisher) : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly IPublisher _publisher = publisher;

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ConvertDomainEventsToBoxMessages();
        UpdateAuditableEntites();

        return _dbContext.SaveChangesAsync(cancellationToken);
    }

    private void ConvertDomainEventsToBoxMessages()
    {
        var outboxMessages = _dbContext.ChangeTracker
            .Entries<AggregateRoot>()
            .Select(x => x.Entity)
            .SelectMany(aggregateRoot => {
                var domainEvents = aggregateRoot.GetDomainEvents();
                foreach (var domainEvent in domainEvents) {
                    _publisher.Publish(domainEvent);
                }
                aggregateRoot.ClearDomainEvents();

                return domainEvents;
            })
            .Select(domainEvent => new OutboxMessage {
                Id = Guid.NewGuid(),
                OccurredOnUtc = DateTime.UtcNow,
                Type = domainEvent.GetType().Name,
                Content = JsonConvert.SerializeObject(
                    domainEvent,
                    new JsonSerializerSettings {
                        TypeNameHandling = TypeNameHandling.All,
                    })
            })
            .ToList();

        _dbContext.Set<OutboxMessage>().AddRange(outboxMessages);
    }

    private void UpdateAuditableEntites()
    {
        IEnumerable<EntityEntry<IAuditableEntity>> entries =
            _dbContext
                .ChangeTracker
                .Entries<IAuditableEntity>();

        foreach (EntityEntry<IAuditableEntity> entityEntry in entries) {
            if (entityEntry.State == Microsoft.EntityFrameworkCore.EntityState.Added) {
                entityEntry.Property(a => a.CreatedOnUtc).CurrentValue = DateTime.UtcNow;
            }

            if (entityEntry.State == Microsoft.EntityFrameworkCore.EntityState.Modified) {
                entityEntry.Property(a => a.ModifiedOnUtc).CurrentValue = DateTime.UtcNow;
            }
        }
    }
}
