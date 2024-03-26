using Cloudea.Domain.Identity.Entities;
using Cloudea.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cloudea.Persistence.Configurations.Identity
{
    public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.ToTable(TableNames.UserProfile);
            builder.HasKey(x => x.AutoIncId);
            builder.HasIndex(x => x.Id);
        }
    }
}
