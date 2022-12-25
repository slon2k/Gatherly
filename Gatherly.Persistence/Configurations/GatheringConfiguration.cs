using Gatherly.Domain.Entities;
using Gatherly.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gatherly.Persistence.Configurations;

internal class GatheringConfiguration : IEntityTypeConfiguration<Gathering>
{
    public void Configure(EntityTypeBuilder<Gathering> builder)
    {
        builder.ToTable("Gathering");

        builder.HasUniqueIdentifier<Gathering, Guid>();

        builder.Property(e => e.Type)
            .IsRequired();
        
        builder.Property(e => e.CreatorId)
            .IsRequired();

        builder.Property(e => e.Name)
            .HasMaxLength(Gathering.NameMaxLength)
            .IsRequired();

        builder.Property(e => e.Location)
            .HasMaxLength(Gathering.LocationMaxLength)
            .IsRequired(false);

        builder.Property(e => e.ScheduledDate)
            .HasColumnType("Date")
            .IsRequired();

        builder.Property(e => e.InvitationsExpireAt)
            .HasColumnType("Date")
            .IsRequired(false);

        builder.Property(e => e.MaxNumberOfAttendees)
            .HasColumnType("int")
            .IsRequired(false);

        builder.HasOne(e => e.Creator)
            .WithMany(c => c.GatheringsCreated)
            .HasForeignKey(e => e.CreatorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
