using Gatherly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<Member> Members { get; set; }

    public DbSet<Gathering> Gatherings { get; set; }

    public DbSet<Attendee> Attendees { get; set; }

    public DbSet<Invitation> Invites { get; set; }
}
