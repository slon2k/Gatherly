using Gatherly.Domain.Entities;

namespace Gatherly.Domain.Repositories;

public interface IGatheringRepository : IRepository<Gathering>
{
    void AddInvitation(Invitation invitation);
}
