using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cloudea.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAnnouncementEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "s_announcement",
                columns: table => new
                {
                    AutoIncId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Content = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatorId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    ModifiedOnUtc = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_s_announcement", x => x.AutoIncId);
                    table.UniqueConstraint("AK_s_announcement_Id", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "s_announcement_translation",
                columns: table => new
                {
                    AutoIncId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Content = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatorId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AnnouncementId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Language = table.Column<string>(type: "longtext", nullable: false),
                    Region = table.Column<string>(type: "longtext", nullable: false),
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_s_announcement_translation", x => x.AutoIncId);
                    table.ForeignKey(
                        name: "FK_s_announcement_translation_s_announcement_AnnouncementId",
                        column: x => x.AnnouncementId,
                        principalTable: "s_announcement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_s_announcement_Id",
                table: "s_announcement",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_s_announcement_translation_AnnouncementId",
                table: "s_announcement_translation",
                column: "AnnouncementId");

            migrationBuilder.CreateIndex(
                name: "IX_s_announcement_translation_Id",
                table: "s_announcement_translation",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "s_announcement_translation");

            migrationBuilder.DropTable(
                name: "s_announcement");
        }
    }
}
