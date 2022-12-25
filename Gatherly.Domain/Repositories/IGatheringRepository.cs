using Gatherly.Domain.Entities;

namespace Gatherly.Domain.Repositories;

public interface IGatheringRepository : IRepository<Gathering>
{
    Task<Gathering?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
