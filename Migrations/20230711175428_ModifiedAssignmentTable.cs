using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TwoDo.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedAssignmentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asignment_Users_UserId",
                table: "Asignment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Asignment",
                table: "Asignment");

            migrationBuilder.RenameTable(
                name: "Asignment",
                newName: "Assignments");

            migrationBuilder.RenameIndex(
                name: "IX_Asignment_UserId",
                table: "Assignments",
                newName: "IX_Assignments_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assignments",
                table: "Assignments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Users_UserId",
                table: "Assignments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Users_UserId",
                table: "Assignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assignments",
                table: "Assignments");

            migrationBuilder.RenameTable(
                name: "Assignments",
                newName: "Asignment");

            migrationBuilder.RenameIndex(
                name: "IX_Assignments_UserId",
                table: "Asignment",
                newName: "IX_Asignment_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Asignment",
                table: "Asignment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Asignment_Users_UserId",
                table: "Asignment",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
