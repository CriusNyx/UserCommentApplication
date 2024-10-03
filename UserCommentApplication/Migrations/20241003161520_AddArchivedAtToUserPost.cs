using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserCommentApplication.Migrations
{
    /// <inheritdoc />
    public partial class AddArchivedAtToUserPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "archivedAt",
                table: "UserPosts",
                type: "timestamp without time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "archivedAt",
                table: "UserPosts");
        }
    }
}
