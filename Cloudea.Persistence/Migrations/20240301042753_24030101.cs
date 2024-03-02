using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cloudea.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _24030101 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_user_role",
                table: "user_role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_login",
                table: "user_login");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user",
                table: "user");

            migrationBuilder.DropPrimaryKey(
                name: "PK_role",
                table: "role");

            migrationBuilder.RenameTable(
                name: "user_role",
                newName: "u_user_role");

            migrationBuilder.RenameTable(
                name: "user_login",
                newName: "u_user_login");

            migrationBuilder.RenameTable(
                name: "user",
                newName: "u_user");

            migrationBuilder.RenameTable(
                name: "role",
                newName: "u_role");

            migrationBuilder.AddPrimaryKey(
                name: "PK_u_user_role",
                table: "u_user_role",
                column: "AutoIncId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_u_user_login",
                table: "u_user_login",
                column: "AutoIncId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_u_user",
                table: "u_user",
                column: "AutoIncId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_u_role",
                table: "u_role",
                column: "Value");

            migrationBuilder.CreateTable(
                name: "u_verification_code",
                columns: table => new
                {
                    AutoIncId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VerCode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VerCodeValidTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    VerCodeType = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModificationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_u_verification_code", x => x.AutoIncId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "u_verification_code");

            migrationBuilder.DropPrimaryKey(
                name: "PK_u_user_role",
                table: "u_user_role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_u_user_login",
                table: "u_user_login");

            migrationBuilder.DropPrimaryKey(
                name: "PK_u_user",
                table: "u_user");

            migrationBuilder.DropPrimaryKey(
                name: "PK_u_role",
                table: "u_role");

            migrationBuilder.RenameTable(
                name: "u_user_role",
                newName: "user_role");

            migrationBuilder.RenameTable(
                name: "u_user_login",
                newName: "user_login");

            migrationBuilder.RenameTable(
                name: "u_user",
                newName: "user");

            migrationBuilder.RenameTable(
                name: "u_role",
                newName: "role");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_role",
                table: "user_role",
                column: "AutoIncId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_login",
                table: "user_login",
                column: "AutoIncId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user",
                table: "user",
                column: "AutoIncId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_role",
                table: "role",
                column: "Value");
        }
    }
}
