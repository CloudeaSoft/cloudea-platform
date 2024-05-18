using Cloudea.Domain.Forum.Entities.Recommend;
using Cloudea.Persistence.Constants;
using Cloudea.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cloudea.Persistence.Configurations.Forum.Recommend;

public class PostSimilarityConfiguration : IEntityTypeConfiguration<PostSimilarity>
{
    public void Configure(EntityTypeBuilder<PostSimilarity> builder)
    {
        builder.ConfigureBase(TableNames.ForumRecommendPostSimilarity);
    }
}