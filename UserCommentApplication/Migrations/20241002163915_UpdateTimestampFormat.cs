using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserCommentApplication.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTimestampFormat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserPostId",
                table: "UserPostEdits");

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdAt",
                table: "UserPostEdits",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "createdAt",
                table: "UserPostEdits",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<int>(
                name: "UserPostId",
                table: "UserPostEdits",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
