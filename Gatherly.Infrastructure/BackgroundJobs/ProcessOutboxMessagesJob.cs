using Gatherly.Domain.Primitives;
using Gatherly.Persistence;
using Gatherly.Persistence.Outbox;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quartz;

namespace Gatherly.Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
public class ProcessOutboxMessagesJob : IJob
{
    private readonly ApplicationDbContext dbContext;
    private readonly IPublisher publisher;

    public ProcessOutboxMessagesJob(ApplicationDbContext dbContext, IPublisher publisher)
    {
        this.dbContext = dbContext;
        this.publisher = publisher;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var messages = await dbContext
            .Set<OutboxMessage>()
            .Where(m => m.ProcessedDateTime == null)
            .Take(20)
            .ToListAsync(context.CancellationToken);

        foreach (var item in messages)
        {
            var domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(
                item.Content,
                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All}
                );

            if (domainEvent is null)
            {
                continue;
            }

            await publisher.Publish(domainEvent, context.CancellationToken);

            item.ProcessedDateTime = DateTime.Now;

            await dbContext.SaveChangesAsync();
        }
    }
}
