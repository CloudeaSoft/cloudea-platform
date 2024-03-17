using Cloudea.Domain.Forum.Entities;
using Cloudea.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cloudea.Persistence.Configurations.Forum;

public class ForumSectionConfiguration : IEntityTypeConfiguration<ForumSection>
{
    public void Configure(EntityTypeBuilder<ForumSection> builder)
    {
        builder.ToTable(TableNames.ForumSection);

        builder.HasKey(x => x.AutoIncId);
    }
}
