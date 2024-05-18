using Cloudea.Domain.Forum.Entities.Recommend;
using Cloudea.Persistence.Constants;
using Cloudea.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cloudea.Persistence.Configurations.Forum.Recommend;

internal class UserSimilarityConfiguration : IEntityTypeConfiguration<UserSimilarity>
{
    public void Configure(EntityTypeBuilder<UserSimilarity> builder)
    {
        builder.ConfigureBase(TableNames.ForumRecommendUserSimilarity);
    }
}
