using Cloudea.Persistence.Constants;
using Cloudea.Service.Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloudea.Persistence.Configurations.Identity
{
    public class VerificationCodeConfiguration : IEntityTypeConfiguration<VerificationCode>
    {
        public void Configure(EntityTypeBuilder<VerificationCode> builder)
        {
            builder.ToTable(TableNames.VerificationCode);
            builder.HasKey(t => t.AutoIncId);
        }
    }
}
