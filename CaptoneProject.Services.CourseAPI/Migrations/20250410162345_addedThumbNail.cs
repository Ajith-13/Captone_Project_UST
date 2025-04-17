using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaptoneProject.Services.CourseAPI.Migrations
{
    /// <inheritdoc />
    public partial class addedThumbNail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ThumbnailImagePath",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThumbnailImagePath",
                table: "Courses");
        }
    }
}
