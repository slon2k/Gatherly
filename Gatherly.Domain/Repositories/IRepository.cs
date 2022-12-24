using Gatherly.Domain.Primitives;

namespace Gatherly.Domain.Repositories;

public interface IRepository<TEntity> where TEntity : AggregateRoot
{
    TEntity? GetById(Guid id);

    IEnumerable<TEntity> GetAll();

    void Create(TEntity entity);

    void Update(TEntity entity);

    void Delete(Guid id);
}
