using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gatherly.Persistence.Repositories;

public class GatheringRepository : RepositoryBase<Gathering>, IGatheringRepository
{
    public GatheringRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Gathering?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Gatherings
            .Include(g => g.Attendees)
            .Include(g => g.Invitations)
            .FirstOrDefaultAsync(g => g.Id.Equals(id), cancellationToken: cancellationToken);
    }
}
