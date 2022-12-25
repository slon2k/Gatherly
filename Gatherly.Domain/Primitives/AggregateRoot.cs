namespace Gatherly.Domain.Primitives;

public abstract class AggregateRoot : Entity
{
    private readonly List<IDomainEvent> domainEvents= new();

    protected AggregateRoot(Guid id) : base(id)
    {
    }

    public IReadOnlyCollection<IDomainEvent> DomainEvents => domainEvents.ToList();

    public void ClearDomainEvents() => domainEvents.Clear();

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        domainEvents.Add(domainEvent);
    }
}
