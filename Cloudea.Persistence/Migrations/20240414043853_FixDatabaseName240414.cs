using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cloudea.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixDatabaseName240414 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_forum_recommend_user_post_interest",
                table: "forum_recommend_user_post_interest");

            migrationBuilder.RenameTable(
                name: "forum_recommend_user_post_interest",
                newName: "forum_comment");

            migrationBuilder.AddPrimaryKey(
                name: "PK_forum_comment",
                table: "forum_comment",
                column: "AutoIncId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_forum_comment",
                table: "forum_comment");

            migrationBuilder.RenameTable(
                name: "forum_comment",
                newName: "forum_recommend_user_post_interest");

            migrationBuilder.AddPrimaryKey(
                name: "PK_forum_recommend_user_post_interest",
                table: "forum_recommend_user_post_interest",
                column: "AutoIncId");
        }
    }
}
