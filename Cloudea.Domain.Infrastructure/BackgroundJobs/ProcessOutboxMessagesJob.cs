using Cloudea.Domain.Common.Primitives;
using Cloudea.Persistence;
using Cloudea.Persistence.Outbox;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;

namespace Cloudea.Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
public class ProcessOutboxMessagesJob : IJob
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<ProcessOutboxMessagesJob> _logger;
    private readonly IPublisher _publisher;

    public ProcessOutboxMessagesJob(ApplicationDbContext dbContext, IPublisher publisher, ILogger<ProcessOutboxMessagesJob> logger)
    {
        _dbContext = dbContext;
        _publisher = publisher;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        List<OutboxMessage> messages = await _dbContext
            .Set<OutboxMessage>()
            .Where(m => m.ProcessedOnUtc == null)
            .Take(20)
            .ToListAsync(context.CancellationToken);


        foreach (OutboxMessage message in messages) {
            IDomainEvent? domainEvent = JsonConvert
                .DeserializeObject<IDomainEvent>(
                    message.Content,
                    new JsonSerializerSettings {
                        TypeNameHandling = TypeNameHandling.All
                    });

            if (domainEvent is null) {
                continue;
            }

            try {
                await _publisher.Publish(domainEvent, context.CancellationToken);
                message.ProcessedOnUtc = DateTimeOffset.UtcNow;
            }
            catch (Exception ex) {
                message.Error = ex.ToString();
                _dbContext.Set<OutboxMessage>().Update(message);
                _logger.LogError(message: ex.Message);
            }
        }

        await _dbContext.SaveChangesAsync();
    }
}
