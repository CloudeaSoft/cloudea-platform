﻿using Cloudea.Domain.Forum.Entities.Recommend;
using Cloudea.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cloudea.Persistence.Configurations.Forum.Recommend;

public class UserPostInterestConfiguration : IEntityTypeConfiguration<UserPostInterest>
{
    public void Configure(EntityTypeBuilder<UserPostInterest> builder)
    {
        builder.ToTable(TableNames.ForumRecommendUserPostInterest);

        builder.HasKey(x => x.AutoIncId);
    }
}
