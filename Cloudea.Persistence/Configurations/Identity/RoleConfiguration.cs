using Cloudea.Domain.Identity.Entities;
using Cloudea.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cloudea.Persistence.Configurations.Identity;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(TableNames.Role);
        builder.HasKey(x => x.Value);
        builder.HasData(
            Role.Admin,
            Role.SubAdmin,
            Role.User);
        builder.Property(x => x.Value);
    }
}
