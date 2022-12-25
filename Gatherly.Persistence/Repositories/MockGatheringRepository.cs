using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;

namespace Gatherly.Persistense.Repositories
{
    public class MockGatheringRepository : IGatheringRepository
    {
        private static readonly HashSet<Gathering> gatherings = new();
        
        public void AddInvitation(Invitation invitation)
        {
            throw new NotImplementedException();
        }

        public void Create(Gathering entity)
        {
            gatherings.Add(entity);
        }

        public void Delete(Guid id)
        {
            var gathering = GetById(id);

            if (gathering is not null)
            {
                gatherings.Remove(gathering);
            }
        }

        public IEnumerable<Gathering> GetAll()
        {
            return gatherings;
        }

        public Gathering? GetById(Guid id)
        {
            return gatherings.FirstOrDefault(x => x.Id.Equals(id));
        }

        public void Update(Gathering entity)
        {
            var gathering = GetById(entity.Id);

            if (gathering is not null)
            {
                gatherings.Remove(gathering);
            }

            gatherings.Add(entity);
        }
    }
}
