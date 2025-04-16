using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaptoneProject.Services.CourseAPI.Migrations
{
    /// <inheritdoc />
    public partial class removedFileType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileType",
                table: "Modules");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileType",
                table: "Modules",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
