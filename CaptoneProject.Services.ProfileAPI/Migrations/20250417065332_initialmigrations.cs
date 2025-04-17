using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaptoneProject.Services.ProfileAPI.Migrations
{
    /// <inheritdoc />
    public partial class initialmigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Profile",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    userName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rollNumber = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rank = table.Column<int>(type: "int", nullable: false),
                    NumberOfCourcesEnrolled = table.Column<int>(type: "int", nullable: false),
                    CoursesNames = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profile", x => x.UserId);
                });

            migrationBuilder.InsertData(
                table: "Profile",
                columns: new[] { "UserId", "CoursesNames", "NumberOfCourcesEnrolled", "PhoneNumber", "email", "rank", "rollNumber", "userName" },
                values: new object[] { "1", "[\"python\",\"C\\u002B\\u002B\"]", 2, "1234567", "Ajith@123", 1, 6655, "Ajith" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Profile");
        }
    }
}
