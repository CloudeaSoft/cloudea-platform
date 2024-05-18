using Cloudea.Domain.System.Entities;
using Cloudea.Persistence.Constants;
using Cloudea.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cloudea.Persistence.Configurations.System;

internal class AnnouncementConfiguration : IEntityTypeConfiguration<Announcement>
{
    public void Configure(EntityTypeBuilder<Announcement> builder)
    {
        builder.ConfigureBase(TableNames.Announcement);

        builder.HasMany(e => e.Translations)
            .WithOne(e => e.Announcement)
            .HasPrincipalKey(e => e.Id)
            .HasForeignKey(e => e.AnnouncementId)
            .IsRequired();

        builder.Navigation(e => e.Translations)
            .UsePropertyAccessMode(PropertyAccessMode.Property);
    }
}
