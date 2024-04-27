using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiringSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class hotfix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "JobSeekers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekers_Email",
                table: "JobSeekers",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JobSeekers_Email",
                table: "JobSeekers");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "JobSeekers");
        }
    }
}
