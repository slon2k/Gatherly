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
}
