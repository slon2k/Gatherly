using Gatherly.Domain.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gatherly.Persistence.Extensions;

internal static class EntityTypeBuilderExtensions
{
    internal static PropertyBuilder HasUniqueIdentifier<TEntity, TKey>(this EntityTypeBuilder<TEntity> builder) where TEntity : Entity where TKey : struct
    {
        builder.HasKey(e => e.Id);

        return builder.Property(e => e.Id)
            .HasColumnName("Id")
            .IsRequired();
    }

    internal static EntityTypeBuilder IsAuditable<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : Entity, IAuditableEntity
    {
        builder.Property(e => e.CreatedAt)
            .HasColumnType("datetime2")
            .IsRequired();

        builder.Property(e => e.UpdatedAt)
            .HasColumnType("datetime2")
            .IsRequired(false);

        return builder;
    }
}
