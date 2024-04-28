using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiringSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class applicationJobseekerId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "JobSeekerId",
                table: "Applications",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Applications_JobSeekerId",
                table: "Applications",
                column: "JobSeekerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_JobSeekers_JobSeekerId",
                table: "Applications",
                column: "JobSeekerId",
                principalTable: "JobSeekers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_JobSeekers_JobSeekerId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_JobSeekerId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "JobSeekerId",
                table: "Applications");
        }
    }
}
