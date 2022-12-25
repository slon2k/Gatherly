using Gatherly.Domain.Entities;
using Gatherly.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gatherly.Persistence.Configurations;

internal class AttendeeConfiguration : IEntityTypeConfiguration<Attendee>
{
    public void Configure(EntityTypeBuilder<Attendee> builder)
    {
        builder.ToTable("Attendee");

        builder.HasUniqueIdentifier<Attendee, Guid>();

        builder.Property(e => e.MemberId)
            .IsRequired();

        builder.Property(e => e.GatheringId)
            .IsRequired();
       
        builder.Property(e => e.CreatedAt)
            .HasColumnType("Date")
            .IsRequired();

        builder.HasOne(e => e.Member)
            .WithMany(e => e.Attendees)
            .HasForeignKey(e => e.MemberId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Gathering)
            .WithMany(e => e.Attendees)
            .HasForeignKey(e => e.GatheringId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(e => new { e.MemberId, e.GatheringId })
            .IsUnique();
    }
}
