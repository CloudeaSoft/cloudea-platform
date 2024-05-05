using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cloudea.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditableInterfaceOnForumLikeAndFavoriteEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedOnUtc",
                table: "forum_post_user_like",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ModifiedOnUtc",
                table: "forum_post_user_like",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedOnUtc",
                table: "forum_post_user_favorite",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ModifiedOnUtc",
                table: "forum_post_user_favorite",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOnUtc",
                table: "forum_post_user_like");

            migrationBuilder.DropColumn(
                name: "ModifiedOnUtc",
                table: "forum_post_user_like");

            migrationBuilder.DropColumn(
                name: "CreatedOnUtc",
                table: "forum_post_user_favorite");

            migrationBuilder.DropColumn(
                name: "ModifiedOnUtc",
                table: "forum_post_user_favorite");
        }
    }
}
