using Cloudea.Domain.Forum.Entities;
using Cloudea.Persistence.Constants;
using Cloudea.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cloudea.Persistence.Configurations.Forum;

internal class ForumPostUserHistoryConfiguration : IEntityTypeConfiguration<ForumPostUserHistory>
{
    public void Configure(EntityTypeBuilder<ForumPostUserHistory> builder)
    {
        builder.ConfigureBase(TableNames.ForumPostUserHistory);
    }
}
