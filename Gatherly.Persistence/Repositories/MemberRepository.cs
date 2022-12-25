using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gatherly.Persistence.Repositories;

public class MemberRepository : IMemberRepository
{
    private readonly ApplicationDbContext context;

    public MemberRepository(ApplicationDbContext context)
    {
        this.context = context;
    }

    public void Create(Member entity)
    {
        context.Members.Add(entity);
        context.SaveChanges();
    }

    public void Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Member> GetAll()
    {
        return context.Members.ToList();
    }

    public Member? GetByEmail(string email)
    {
        return context.Members.FirstOrDefault(x => x.Email == email);
    }

    public Member? GetById(Guid id)
    {
        return context.Members.Find(id);
    }

    public void Update(Member entity)
    {
        context.Entry(entity).State = EntityState.Modified;
        context.SaveChanges();
    }
}
