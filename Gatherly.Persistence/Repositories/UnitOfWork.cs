using Gatherly.Domain.Primitives;
using Gatherly.Domain.Repositories;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

namespace Gatherly.Persistence.Repositories;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext dbContext;

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditableEntities();

        return dbContext.SaveChangesAsync(cancellationToken);
    }

    private void UpdateAuditableEntities()
    {
        IEnumerable<EntityEntry<IAuditableEntity>> entities = dbContext.ChangeTracker.Entries<IAuditableEntity>();

        foreach (var entity in entities)
        {
            if (entity.State == EntityState.Modified)
            {
                entity.Property(e => e.UpdatedAt).CurrentValue = DateTime.UtcNow;
            }

            if (entity.State == EntityState.Added)
            {
                entity.Property(e => e.CreatedAt).CurrentValue = DateTime.UtcNow;
            }
        }
    }
}
