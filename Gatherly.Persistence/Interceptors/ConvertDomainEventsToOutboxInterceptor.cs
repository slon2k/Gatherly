using Gatherly.Domain.Primitives;
using Gatherly.Persistence.Outbox;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gatherly.Persistence.Interceptors;

public sealed class ConvertDomainEventsToOutboxInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        var dbContext = eventData.Context;

        if (dbContext is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        var events = dbContext.ChangeTracker
            .Entries<AggregateRoot>()
            .Select(x => x.Entity)
            .SelectMany(aggregateRoot =>
            {
                var domainEvents = aggregateRoot.DomainEvents;
                aggregateRoot.ClearDomainEvents();
                return domainEvents;
            }).Select(domainEvent => new OutboxMessage 
            {
                Id = Guid.NewGuid(),
                OccuredDateTime= DateTime.UtcNow,
                Type = domainEvent.GetType().Name,
                Content = JsonConvert.SerializeObject(
                    domainEvent,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling= TypeNameHandling.All,
                    })
            })
            .ToList();
            
            dbContext.Set<OutboxMessage>().AddRange(events);
            
            return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
