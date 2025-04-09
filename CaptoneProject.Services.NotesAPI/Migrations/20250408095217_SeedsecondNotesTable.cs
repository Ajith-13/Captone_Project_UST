using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaptoneProject.Services.NotesAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedsecondNotesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Notes",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "Id", "DateCreated", "DateModified", "Description", "Resources", "Title", "UserId" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Python programming is a high-level, general-purpose, interpreted, object-oriented programming language known for its readability and versatility, used for various tasks like web development, data analysis, and software development.", "GeeksForGeeks", "Python", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Notes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Notes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
