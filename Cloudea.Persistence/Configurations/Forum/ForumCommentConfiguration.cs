using Cloudea.Domain.Forum.Entities;
using Cloudea.Persistence.Constants;
using Cloudea.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cloudea.Persistence.Configurations.Forum;

internal class ForumCommentConfiguration : IEntityTypeConfiguration<ForumComment>
{
    public void Configure(EntityTypeBuilder<ForumComment> builder)
    {
        builder.ConfigureBase(TableNames.ForumComment);
    }
}
