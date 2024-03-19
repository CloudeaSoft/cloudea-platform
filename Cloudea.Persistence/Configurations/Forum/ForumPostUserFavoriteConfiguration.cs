using Cloudea.Domain.Forum.Entities;
using Cloudea.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cloudea.Persistence.Configurations.Forum;

internal class ForumPostUserFavoriteConfiguration : IEntityTypeConfiguration<ForumPostUserFavorite>
{
    public void Configure(EntityTypeBuilder<ForumPostUserFavorite> builder)
    {
        builder.ToTable(TableNames.ForumPostUserFavorite);

        builder.HasKey(x => x.AutoIncId);
    }
}
