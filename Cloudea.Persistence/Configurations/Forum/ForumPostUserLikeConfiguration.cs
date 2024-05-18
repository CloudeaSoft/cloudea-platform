using Cloudea.Domain.Forum.Entities;
using Cloudea.Persistence.Constants;
using Cloudea.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cloudea.Persistence.Configurations.Forum;

internal class ForumPostUserLikeConfiguration : IEntityTypeConfiguration<ForumPostUserLike>
{
    public void Configure(EntityTypeBuilder<ForumPostUserLike> builder)
    {
        builder.ConfigureBase(TableNames.ForumPostUserLike);
    }
}
