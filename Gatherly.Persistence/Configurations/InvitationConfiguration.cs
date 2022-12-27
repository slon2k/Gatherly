using Gatherly.Domain.Entities;
using Gatherly.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gatherly.Persistence.Configurations;

internal class InvitationConfiguration : IEntityTypeConfiguration<Invitation>
{
    public void Configure(EntityTypeBuilder<Invitation> builder)
    {
        builder.ToTable("Invitation");

        builder.HasUniqueIdentifier<Invitation, Guid>();

        builder.Property(e => e.MemberId)
            .IsRequired();

        builder.Property(e => e.GatheringId)
            .IsRequired();

        builder.Property(e => e.Status)
            .IsRequired();

        builder.IsAuditable();
        //builder.Property(e => e.CreatedAt)
        //    .HasColumnType("datetime2")
        //    .IsRequired();

        //builder.Property(e => e.UpdatedAt)
        //    .HasColumnType("datetime2")
        //    .IsRequired(false);

        builder.HasOne(e => e.Member)
            .WithMany(e => e.Invitations)
            .HasForeignKey(e => e.MemberId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Gathering)
            .WithMany(e => e.Invitations)
            .HasForeignKey(e => e.GatheringId)
            .OnDelete(DeleteBehavior.Restrict);


    }
}
