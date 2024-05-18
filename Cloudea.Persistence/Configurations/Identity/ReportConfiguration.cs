using Cloudea.Domain.Identity.Entities;
using Cloudea.Persistence.Constants;
using Cloudea.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cloudea.Persistence.Configurations.Identity;

internal class ReportConfiguration : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        builder.ConfigureBase(TableNames.Report);
    }
}
