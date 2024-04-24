using Cloudea.Domain.Forum.Entities.Recommend;
using Cloudea.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cloudea.Persistence.Configurations.Forum.Recommend;

public class UserSimilarityConfiguration : IEntityTypeConfiguration<UserSimilarity>
{
    public void Configure(EntityTypeBuilder<UserSimilarity> builder)
    {
        builder.ToTable(TableNames.ForumRecommendUserSimilarity);

        builder.HasKey(x => x.AutoIncId);
    }
}
