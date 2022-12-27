using Gatherly.Domain.Primitives;

namespace Gatherly.Domain.Repositories;

public interface IRepository<TEntity> where TEntity : AggregateRoot
{
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<TEntity?> FindAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> AnyAsync(Guid id, CancellationToken cancellationToken = default);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    
    void Delete(TEntity entity);

    void Update(TEntity entity);
}
