﻿// <auto-generated />
using System;
using Cloudea.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Cloudea.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240226084807_new24022601")]
    partial class new24022601
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Cloudea.Persistence.Outbox.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Error")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("OccurredOnUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("ProcessedOnUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("outbox_messages", (string)null);
                });

            modelBuilder.Entity("Cloudea.Service.Forum.Domain.Entities.ForumComment", b =>
                {
                    b.Property<long>("AutoIncId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<long>("LikeCount")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("OwnerUserId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ParentReplyId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("TargetUserId")
                        .HasColumnType("char(36)");

                    b.HasKey("AutoIncId");

                    b.ToTable("forum_comment", (string)null);
                });

            modelBuilder.Entity("Cloudea.Service.Forum.Domain.Entities.ForumPost", b =>
                {
                    b.Property<long>("AutoIncId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("ClickCount")
                        .HasColumnType("bigint");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("LastClickTime")
                        .HasColumnType("timestamp");

                    b.Property<DateTime>("LastEditTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("OwnerUserId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ParentSectionId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("AutoIncId");

                    b.ToTable("forum_post", (string)null);
                });

            modelBuilder.Entity("Cloudea.Service.Forum.Domain.Entities.ForumReply", b =>
                {
                    b.Property<long>("AutoIncId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<long>("LikeCount")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("OwnerUserId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ParentPostId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.HasKey("AutoIncId");

                    b.ToTable("forum_reply", (string)null);
                });

            modelBuilder.Entity("Cloudea.Service.Forum.Domain.Entities.ForumSection", b =>
                {
                    b.Property<long>("AutoIncId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("ClickCount")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("MasterUserId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Statement")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long>("TopicCount")
                        .HasColumnType("bigint");

                    b.HasKey("AutoIncId");

                    b.ToTable("forum_section", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
