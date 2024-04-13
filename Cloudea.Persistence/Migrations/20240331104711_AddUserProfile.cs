using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cloudea.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUserProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_forum_comment",
                table: "forum_comment");

            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "u_user");

            migrationBuilder.DropColumn(
                name: "NickName",
                table: "u_user");

            migrationBuilder.RenameTable(
                name: "forum_comment",
                newName: "forum_recommend_user_post_interest");

            migrationBuilder.AddPrimaryKey(
                name: "PK_forum_recommend_user_post_interest",
                table: "forum_recommend_user_post_interest",
                column: "AutoIncId");

            migrationBuilder.CreateTable(
                name: "u_profile",
                columns: table => new
                {
                    AutoIncId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DisplayName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Signature = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AvatarUrl = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CoverImageUrl = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Leaves = table.Column<int>(type: "int", nullable: false),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    ModifiedOnUtc = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_u_profile", x => x.AutoIncId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_u_profile_Id",
                table: "u_profile",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "u_profile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_forum_recommend_user_post_interest",
                table: "forum_recommend_user_post_interest");

            migrationBuilder.RenameTable(
                name: "forum_recommend_user_post_interest",
                newName: "forum_comment");

            migrationBuilder.AddColumn<byte[]>(
                name: "Avatar",
                table: "u_user",
                type: "varbinary(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NickName",
                table: "u_user",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_forum_comment",
                table: "forum_comment",
                column: "AutoIncId");
        }
    }
}
