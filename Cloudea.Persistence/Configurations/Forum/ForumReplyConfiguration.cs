using Cloudea.Persistence.Constants;
using Cloudea.Service.Forum.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cloudea.Persistence.Configurations.Forum;

public class ForumReplyConfiguration : IEntityTypeConfiguration<ForumReply>
{
    public void Configure(EntityTypeBuilder<ForumReply> builder)
    {
        builder.ToTable(TableNames.ForumReply);

        builder.HasKey(x => x.AutoIncId);
    }
}
