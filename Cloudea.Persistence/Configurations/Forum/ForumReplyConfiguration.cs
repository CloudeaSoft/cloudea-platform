using Cloudea.Domain.Forum.Entities;
using Cloudea.Persistence.Constants;
using Cloudea.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cloudea.Persistence.Configurations.Forum;

internal class ForumReplyConfiguration : IEntityTypeConfiguration<ForumReply>
{
    public void Configure(EntityTypeBuilder<ForumReply> builder)
    {
        builder.ConfigureBase(TableNames.ForumReply);
    }
}
