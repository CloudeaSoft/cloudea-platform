﻿using Cloudea.Domain.Forum.Entities;
using Cloudea.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Cloudea.Persistence.Configurations.Forum
{
    internal class ForumPostConfiguration : IEntityTypeConfiguration<ForumPost>
    {
        public void Configure(EntityTypeBuilder<ForumPost> builder)
        {
            builder.ToTable(TableNames.ForumPost);

            builder.HasKey(x => x.AutoIncId);

            builder.Property(m => m.LastClickTime).IsRequired().HasColumnType("timestamp");
        }
    }
}