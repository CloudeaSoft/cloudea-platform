using Cloudea.Persistence.Constants;
using Cloudea.Service.Forum.Domain.Entities;
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
