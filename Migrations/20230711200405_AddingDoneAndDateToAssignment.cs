using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TwoDo.Migrations
{
    /// <inheritdoc />
    public partial class AddingDoneAndDateToAssignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CompleteDate",
                table: "Assignments",
                type: "Date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDone",
                table: "Assignments",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompleteDate",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "IsDone",
                table: "Assignments");
        }
    }
}
