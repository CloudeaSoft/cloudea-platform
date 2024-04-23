using Cloudea.Domain.Identity.Entities;
using Cloudea.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cloudea.Persistence.Configurations.Identity
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(TableNames.User);
            builder.HasKey(t => t.AutoIncId);

            builder.ComplexProperty(u => u.PasswordHash)
                .Property(p => p.Value)
                .HasColumnName(nameof(User.PasswordHash));


            builder.ComplexProperty(u => u.Salt)
                .Property(s => s.Value)
                .HasColumnName(nameof(User.Salt));
        }
    }
}
