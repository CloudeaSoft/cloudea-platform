﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Cloudea.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Cloudea.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240518114539_RefractorConfigurations")]
    partial class RefractorConfigurations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Cloudea.Domain.File.Entities.UploadedFile", b =>
                {
                    b.Property<long>("AutoIncId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("AutoIncId"));

                    b.Property<string>("BackupUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FileSHA256Hash")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long>("FileSizeInBytes")
                        .HasColumnType("bigint");

                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<string>("RemoteUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("AutoIncId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("f_file", (string)null);
                });

            modelBuilder.Entity("Cloudea.Domain.Forum.Entities.ForumComment", b =>
                {
                    b.Property<long>("AutoIncId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("AutoIncId"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTimeOffset>("CreatedOnUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("DislikeCount")
                        .HasColumnType("bigint");

                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<long>("LikeCount")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("ModifiedOnUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("OwnerUserId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ParentReplyId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("TargetUserId")
                        .HasColumnType("char(36)");

                    b.HasKey("AutoIncId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("forum_comment", (string)null);
                });

            modelBuilder.Entity("Cloudea.Domain.Forum.Entities.ForumPost", b =>
                {
                    b.Property<long>("AutoIncId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("AutoIncId"));

                    b.Property<long>("ClickCount")
                        .HasColumnType("bigint");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTimeOffset>("CreatedOnUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("DislikeCount")
                        .HasColumnType("bigint");

                    b.Property<long>("FavoriteCount")
                        .HasColumnType("bigint");

                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<DateTimeOffset>("LastClickTime")
                        .HasColumnType("timestamp");

                    b.Property<DateTimeOffset>("LastEditTime")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("LikeCount")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("ModifiedOnUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("OwnerUserId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ParentSectionId")
                        .HasColumnType("char(36)");

                    b.Property<long>("ReplyCount")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("AutoIncId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("forum_post", (string)null);
                });

            modelBuilder.Entity("Cloudea.Domain.Forum.Entities.ForumPostUserFavorite", b =>
                {
                    b.Property<long>("AutoIncId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("AutoIncId"));

                    b.Property<DateTimeOffset>("CreatedOnUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<DateTimeOffset?>("ModifiedOnUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("OwnerUserId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ParentPostId")
                        .HasColumnType("char(36)");

                    b.HasKey("AutoIncId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("forum_post_user_favorite", (string)null);
                });

            modelBuilder.Entity("Cloudea.Domain.Forum.Entities.ForumPostUserHistory", b =>
                {
                    b.Property<long>("AutoIncId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("AutoIncId"));

                    b.Property<DateTimeOffset>("CreatedOnUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<DateTimeOffset?>("ModifiedOnUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("PostId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("AutoIncId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("forum_post_user_history", (string)null);
                });

            modelBuilder.Entity("Cloudea.Domain.Forum.Entities.ForumPostUserLike", b =>
                {
                    b.Property<long>("AutoIncId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("AutoIncId"));

                    b.Property<DateTimeOffset>("CreatedOnUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsLike")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("ModifiedOnUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("OwnerUserId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ParentPostId")
                        .HasColumnType("char(36)");

                    b.HasKey("AutoIncId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("forum_post_user_like", (string)null);
                });

            modelBuilder.Entity("Cloudea.Domain.Forum.Entities.ForumReply", b =>
                {
                    b.Property<long>("AutoIncId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("AutoIncId"));

                    b.Property<long>("CommentCount")
                        .HasColumnType("bigint");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTimeOffset>("CreatedOnUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("DislikeCount")
                        .HasColumnType("bigint");

                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<long>("LikeCount")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("ModifiedOnUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("OwnerUserId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ParentPostId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.HasKey("AutoIncId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("forum_reply", (string)null);
                });

            modelBuilder.Entity("Cloudea.Domain.Forum.Entities.ForumSection", b =>
                {
                    b.Property<long>("AutoIncId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("AutoIncId"));

                    b.Property<long>("ClickCount")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("CreatedOnUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("MasterUserId")
                        .HasColumnType("char(36)");

                    b.Property<DateTimeOffset?>("ModifiedOnUtc")
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

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("forum_section", (string)null);
                });

            modelBuilder.Entity("Cloudea.Domain.Forum.Entities.Recommend.PostSimilarity", b =>
                {
                    b.Property<long>("AutoIncId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("AutoIncId"));

                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("PostId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("RelatedPostId")
                        .HasColumnType("char(36)");

                    b.Property<double>("Score")
                        .HasColumnType("double");

                    b.HasKey("AutoIncId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("forum_recommend_post_similarity", (string)null);
                });

            modelBuilder.Entity("Cloudea.Domain.Forum.Entities.Recommend.UserPostInterest", b =>
                {
                    b.Property<long>("AutoIncId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("AutoIncId"));

                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("PostId")
                        .HasColumnType("char(36)");

                    b.Property<double>("Score")
                        .HasColumnType("double");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("AutoIncId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("forum_recommend_user_post_interest", (string)null);
                });

            modelBuilder.Entity("Cloudea.Domain.Forum.Entities.Recommend.UserSimilarity", b =>
                {
                    b.Property<long>("AutoIncId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("AutoIncId"));

                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("RelatedUserId")
                        .HasColumnType("char(36)");

                    b.Property<double>("Score")
                        .HasColumnType("double");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("AutoIncId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("forum_recommend_user_similarity", (string)null);
                });

            modelBuilder.Entity("Cloudea.Domain.Identity.Entities.Report", b =>
                {
                    b.Property<long>("AutoIncId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("AutoIncId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("TargetItemId")
                        .HasColumnType("char(36)");

                    b.Property<int>("TargetItemType")
                        .HasColumnType("int");

                    b.Property<Guid>("TargetUserId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("AutoIncId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("u_report", (string)null);
                });

            modelBuilder.Entity("Cloudea.Domain.Identity.Entities.Role", b =>
                {
                    b.Property<int>("Value")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Value"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Permissions")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Value");

                    b.ToTable("u_role", (string)null);

                    b.HasData(
                        new
                        {
                            Value = 2,
                            Name = "Admin",
                            Permissions = "[]"
                        },
                        new
                        {
                            Value = 3,
                            Name = "SubAdmin",
                            Permissions = "[]"
                        },
                        new
                        {
                            Value = 1,
                            Name = "User",
                            Permissions = "[]"
                        });
                });

            modelBuilder.Entity("Cloudea.Domain.Identity.Entities.User", b =>
                {
                    b.Property<long>("AutoIncId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("AutoIncId"));

                    b.Property<DateTimeOffset?>("DeletionTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Enable")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.ComplexProperty<Dictionary<string, object>>("PasswordHash", "Cloudea.Domain.Identity.Entities.User.PasswordHash#PasswordHash", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("longtext")
                                .HasColumnName("PasswordHash");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Salt", "Cloudea.Domain.Identity.Entities.User.Salt#Salt", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("longtext")
                                .HasColumnName("Salt");
                        });

                    b.HasKey("AutoIncId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("u_user", (string)null);
                });

            modelBuilder.Entity("Cloudea.Domain.Identity.Entities.UserLogin", b =>
                {
                    b.Property<long>("AutoIncId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("AutoIncId"));

                    b.Property<string>("Date")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Hour")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("varchar(2)");

                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("AutoIncId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("u_user_login", (string)null);
                });

            modelBuilder.Entity("Cloudea.Domain.Identity.Entities.UserProfile", b =>
                {
                    b.Property<long>("AutoIncId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("AutoIncId"));

                    b.Property<string>("AvatarUrl")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("CoverImageUrl")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<DateTimeOffset>("CreatedOnUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<int>("Leaves")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("ModifiedOnUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Signature")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("AutoIncId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("u_profile", (string)null);
                });

            modelBuilder.Entity("Cloudea.Domain.Identity.Entities.UserRole", b =>
                {
                    b.Property<long>("AutoIncId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("AutoIncId"));

                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("AutoIncId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("u_user_role", (string)null);
                });

            modelBuilder.Entity("Cloudea.Domain.Identity.Entities.VerificationCode", b =>
                {
                    b.Property<long>("AutoIncId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("AutoIncId"));

                    b.Property<DateTimeOffset>("CreatedOnUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<DateTimeOffset?>("ModifiedOnUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("VerCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("VerCodeType")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("VerCodeValidTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("AutoIncId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("u_verification_code", (string)null);
                });

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

                    b.Property<DateTimeOffset>("OccurredOnUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset?>("ProcessedOnUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("outbox_messages", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
