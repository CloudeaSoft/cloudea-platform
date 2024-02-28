using Cloudea.Infrastructure.Primitives;
using Cloudea.Persistence.Outbox;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quartz;

namespace Cloudea.Persistence.BackgroundJobs;

[DisallowConcurrentExecution]
public class ProcessOutboxMessagesJob : IJob
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IPublisher _publisher;

    public ProcessOutboxMessagesJob(ApplicationDbContext dbContext, IPublisher publisher)
    {
        _dbContext = dbContext;
        _publisher = publisher;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var messages = await _dbContext
            .Set<OutboxMessage>()
            .Where(m => m.ProcessedOnUtc == null)
            .Take(20)
            .ToListAsync(context.CancellationToken);

        try {
            foreach (var message in messages) {
                var domainEvent = JsonConvert
                    .DeserializeObject<IDomainEvent>(message.Content);

                if (domainEvent is null) {
                    continue;
                }

                await _publisher.Publish(domainEvent, context.CancellationToken);

                message.ProcessedOnUtc = DateTime.UtcNow;
            }
        }
        catch (Exception ex) {

        }

        await _dbContext.SaveChangesAsync();
    }
}
