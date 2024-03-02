using Cloudea.Persistence.Constants;
using Cloudea.Service.Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cloudea.Persistence.Configurations.Identity
{
    public class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {
            builder.ToTable(TableNames.UserLogin);
            builder.HasKey(t => t.AutoIncId);
        }
    }
}
