using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cloudea.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RefractorConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_u_verification_code_Id",
                table: "u_verification_code",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_u_user_role_Id",
                table: "u_user_role",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_u_user_login_Id",
                table: "u_user_login",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_u_user_Id",
                table: "u_user",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_u_report_Id",
                table: "u_report",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_u_profile_Id",
                table: "u_profile",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_forum_section_Id",
                table: "forum_section",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_forum_reply_Id",
                table: "forum_reply",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_forum_recommend_user_similarity_Id",
                table: "forum_recommend_user_similarity",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_forum_recommend_user_post_interest_Id",
                table: "forum_recommend_user_post_interest",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_forum_recommend_post_similarity_Id",
                table: "forum_recommend_post_similarity",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_forum_post_user_like_Id",
                table: "forum_post_user_like",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_forum_post_user_history_Id",
                table: "forum_post_user_history",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_forum_post_user_favorite_Id",
                table: "forum_post_user_favorite",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_forum_post_Id",
                table: "forum_post",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_forum_comment_Id",
                table: "forum_comment",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_f_file_Id",
                table: "f_file",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_u_verification_code_Id",
                table: "u_verification_code");

            migrationBuilder.DropIndex(
                name: "IX_u_user_role_Id",
                table: "u_user_role");

            migrationBuilder.DropIndex(
                name: "IX_u_user_login_Id",
                table: "u_user_login");

            migrationBuilder.DropIndex(
                name: "IX_u_user_Id",
                table: "u_user");

            migrationBuilder.DropIndex(
                name: "IX_u_report_Id",
                table: "u_report");

            migrationBuilder.DropIndex(
                name: "IX_u_profile_Id",
                table: "u_profile");

            migrationBuilder.DropIndex(
                name: "IX_forum_section_Id",
                table: "forum_section");

            migrationBuilder.DropIndex(
                name: "IX_forum_reply_Id",
                table: "forum_reply");

            migrationBuilder.DropIndex(
                name: "IX_forum_recommend_user_similarity_Id",
                table: "forum_recommend_user_similarity");

            migrationBuilder.DropIndex(
                name: "IX_forum_recommend_user_post_interest_Id",
                table: "forum_recommend_user_post_interest");

            migrationBuilder.DropIndex(
                name: "IX_forum_recommend_post_similarity_Id",
                table: "forum_recommend_post_similarity");

            migrationBuilder.DropIndex(
                name: "IX_forum_post_user_like_Id",
                table: "forum_post_user_like");

            migrationBuilder.DropIndex(
                name: "IX_forum_post_user_history_Id",
                table: "forum_post_user_history");

            migrationBuilder.DropIndex(
                name: "IX_forum_post_user_favorite_Id",
                table: "forum_post_user_favorite");

            migrationBuilder.DropIndex(
                name: "IX_forum_post_Id",
                table: "forum_post");

            migrationBuilder.DropIndex(
                name: "IX_forum_comment_Id",
                table: "forum_comment");

            migrationBuilder.DropIndex(
                name: "IX_f_file_Id",
                table: "f_file");
        }
    }
}
