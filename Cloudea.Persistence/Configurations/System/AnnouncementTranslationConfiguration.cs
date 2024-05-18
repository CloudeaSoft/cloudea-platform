using Cloudea.Domain.System.Entities;
using Cloudea.Domain.System.ValueObjects;
using Cloudea.Persistence.Constants;
using Cloudea.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cloudea.Persistence.Configurations.System;

internal class AnnouncementTranslationConfiguration : IEntityTypeConfiguration<AnnouncementTranslation>
{
    public void Configure(EntityTypeBuilder<AnnouncementTranslation> builder)
    {
        builder.ConfigureBase(TableNames.AnnouncementTranslation);

        builder.Navigation(e=>e.Announcement)
            .UsePropertyAccessMode(PropertyAccessMode.Property);

        builder.ComplexProperty(x => x.LanguageCode)
            .Property(c => c.Language)
            .HasColumnName(nameof(LanguageCode.Language));

        builder.ComplexProperty(x => x.LanguageCode)
            .Property(c => c.Region)
            .HasColumnName(nameof(LanguageCode.Region));
    }
}
