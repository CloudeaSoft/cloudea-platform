using Cloudea.Domain.Forum.Entities.Recommend;
using Cloudea.Persistence.Constants;
using Cloudea.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cloudea.Persistence.Configurations.Forum.Recommend;

internal class UserPostInterestConfiguration : IEntityTypeConfiguration<UserPostInterest>
{
    public void Configure(EntityTypeBuilder<UserPostInterest> builder)
    {
        builder.ConfigureBase(TableNames.ForumRecommendUserPostInterest);
    }
}
