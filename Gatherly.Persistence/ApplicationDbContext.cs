using Gatherly.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gatherly.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected ApplicationDbContext()
    {
    }

    public DbSet<Member> Members { get; set; }

    public DbSet<Gathering> Gatherings { get; set; }

    public DbSet<Attendee> Attendees { get; set; }

    public DbSet<Invitation> Invites { get; set; }
}
