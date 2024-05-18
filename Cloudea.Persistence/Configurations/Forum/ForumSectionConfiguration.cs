using Cloudea.Domain.Forum.Entities;
using Cloudea.Persistence.Constants;
using Cloudea.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cloudea.Persistence.Configurations.Forum;

internal class ForumSectionConfiguration : IEntityTypeConfiguration<ForumSection>
{
    public void Configure(EntityTypeBuilder<ForumSection> builder)
    {
        builder.ConfigureBase(TableNames.ForumSection);
    }
}
