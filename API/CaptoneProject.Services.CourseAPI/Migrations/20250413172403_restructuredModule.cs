using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaptoneProject.Services.CourseAPI.Migrations
{
    /// <inheritdoc />
    public partial class restructuredModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentData",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Modules");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Modules",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Modules",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileType",
                table: "Modules",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "FileType",
                table: "Modules");

            migrationBuilder.AddColumn<string>(
                name: "ContentData",
                table: "Modules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ContentType",
                table: "Modules",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
