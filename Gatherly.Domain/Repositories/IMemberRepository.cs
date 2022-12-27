using Gatherly.Domain.Entities;

namespace Gatherly.Domain.Repositories;

public interface IMemberRepository : IRepository<Member>
{
    Task<Member?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
}
