using Gatherly.Domain.Primitives;
using Gatherly.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gatherly.Persistence.Repositories;

public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : AggregateRoot
{
    protected readonly ApplicationDbContext context;

    public RepositoryBase(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await context.Set<TEntity>().AddAsync(entity, cancellationToken);
    }

    public async Task<bool> AnyAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Set<TEntity>().AnyAsync(x => id.Equals(x.Id), cancellationToken);
    }

    public void Delete(TEntity entity)
    {
        if (entity is not null)
        {
            context.Set<TEntity>().Remove(entity);
        }
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (await FindAsync(id, cancellationToken) is TEntity entity)
        {
            Delete(entity);
        }
    }

    public async Task<TEntity?> FindAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Set<TEntity>().ToListAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }

    public void Update(TEntity entity)
    {
        context.Entry(entity).State = EntityState.Modified;
    }
}
