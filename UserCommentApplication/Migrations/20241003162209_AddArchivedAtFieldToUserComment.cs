using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserCommentApplication.Migrations
{
    /// <inheritdoc />
    public partial class AddArchivedAtFieldToUserComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "archivedAt",
                table: "UserComments",
                type: "timestamp without time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "archivedAt",
                table: "UserComments");
        }
    }
}
