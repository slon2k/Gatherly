namespace Gatherly.Domain.Primitives;

public interface IAuditableEntity
{
    DateTime CreatedAt { get; }

    DateTime? UpdatedAt { get; }
}
