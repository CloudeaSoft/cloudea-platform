using Cloudea.Domain.Common.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cloudea.Persistence.Extensions;

internal static class EntityTypeBuilderExtensions
{
    public static void ConfigureBase<TEntity>(this EntityTypeBuilder<TEntity> builder, string tablename)
        where TEntity : BaseDataEntity
    {
        builder.ToTable(tablename);
        builder.HasIndex(p => p.Id).IsUnique();
        builder.HasKey(e => e.AutoIncId);
    }
}
