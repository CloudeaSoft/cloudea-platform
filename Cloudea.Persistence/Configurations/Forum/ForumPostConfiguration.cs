using Cloudea.Domain.Forum.Entities;
using Cloudea.Persistence.Constants;
using Cloudea.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Cloudea.Persistence.Configurations.Forum;

internal class ForumPostConfiguration : IEntityTypeConfiguration<ForumPost>
{
    public void Configure(EntityTypeBuilder<ForumPost> builder)
    {
        builder.ConfigureBase(TableNames.ForumPost);

        builder.Property(m => m.LastClickTime).IsRequired().HasColumnType("timestamp");
    }
}
