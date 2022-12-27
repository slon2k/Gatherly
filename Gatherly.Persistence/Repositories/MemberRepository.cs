using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gatherly.Persistence.Repositories;

public class MemberRepository : RepositoryBase<Member>, IMemberRepository
{
    public MemberRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Member?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await context.Members.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }
}
