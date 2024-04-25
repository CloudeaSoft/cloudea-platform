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
    private readonly IMediator _mediator;

    public ProcessOutboxMessagesJob(ApplicationDbContext dbContext, IMediator mediator, ILogger<ProcessOutboxMessagesJob> logger)
    {
        _dbContext = dbContext;
        _mediator = mediator;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        List<OutboxMessage> messages = await _dbContext
            .Set<OutboxMessage>()
            .Where(m => m.ProcessedOnUtc == null)
            .OrderBy(x => x.OccurredOnUtc)
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
                await _mediator.Publish(domainEvent, context.CancellationToken);
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
