using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cloudea.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ForumChanges240315 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DislikeCount",
                table: "forum_reply",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DislikeCount",
                table: "forum_post",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "LikeCount",
                table: "forum_post",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DislikeCount",
                table: "forum_comment",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DislikeCount",
                table: "forum_reply");

            migrationBuilder.DropColumn(
                name: "DislikeCount",
                table: "forum_post");

            migrationBuilder.DropColumn(
                name: "LikeCount",
                table: "forum_post");

            migrationBuilder.DropColumn(
                name: "DislikeCount",
                table: "forum_comment");
        }
    }
}
