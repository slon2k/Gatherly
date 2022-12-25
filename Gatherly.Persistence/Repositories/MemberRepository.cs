using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gatherly.Persistence.Repositories;

public class MemberRepository : RepositoryBase<Member>, IMemberRepository
{
    public MemberRepository(ApplicationDbContext context) : base(context)
    {
    }

    public Member? GetByEmail(string email)
    {
        return context.Members.FirstOrDefault(x => x.Email == email);
    }
}
