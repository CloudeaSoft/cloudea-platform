using Cloudea.Domain.Identity.Entities;
using Cloudea.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cloudea.Persistence.Configurations.Identity
{
    internal class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.ToTable(TableNames.Report);

            builder.HasKey(x => x.AutoIncId);
        }
    }
}
