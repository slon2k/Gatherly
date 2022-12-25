using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;

namespace Gatherly.Persistence.Repositories;

public class MockMemberRepository : IMemberRepository
{
    private static readonly HashSet<Member> members= new();

    public void Create(Member entity)
    {
        var member = GetById(entity.Id);

        if (member is null)
        {
            members.Add(entity);
        }
    }

    public void Delete(Guid id)
    {
        var member = GetById(id);

        if (member is not null)
        {
            members.Remove(member);
        }
    }

    public IEnumerable<Member> GetAll()
    {
        return members;
    }

    public Member? GetByEmail(string email)
    {
        return members.FirstOrDefault(x => x.Email.ToLower().Equals(email.ToLower()));
    }

    public Member? GetById(Guid id)
    {
        return members.FirstOrDefault(x => x.Id.Equals(id));
    }

    public void Update(Member entity)
    {
        var member = GetById(entity.Id);

        if (member is not null)
        {
            members.Remove(member);
        }

        members.Add(entity);
    }
}
