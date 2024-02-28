using Cloudea.Persistence.Constants;
using Cloudea.Service.Forum.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cloudea.Persistence.Configurations;

public class ForumCommentConfiguration : IEntityTypeConfiguration<ForumComment>
{
    public void Configure(EntityTypeBuilder<ForumComment> builder)
    {
        builder.ToTable(TableNames.ForumComment);

        builder.HasKey(x => x.AutoIncId);
    }
}
