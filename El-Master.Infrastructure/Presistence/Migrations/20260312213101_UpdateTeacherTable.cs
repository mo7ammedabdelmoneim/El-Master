using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace El_Master.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTeacherTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Teachers",
                newName: "FirstName");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Teachers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_ApplicationUserId",
                table: "Teachers",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_AspNetUsers_ApplicationUserId",
                table: "Teachers",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_AspNetUsers_ApplicationUserId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_ApplicationUserId",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Teachers");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Teachers",
                newName: "Name");
        }
    }
}
