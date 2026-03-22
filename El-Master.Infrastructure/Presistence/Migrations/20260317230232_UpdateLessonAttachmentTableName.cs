using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace El_Master.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLessonAttachmentTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lessonAttachments_Lessons_LessonId",
                table: "lessonAttachments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_lessonAttachments",
                table: "lessonAttachments");

            migrationBuilder.RenameTable(
                name: "lessonAttachments",
                newName: "LessonAttachments");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Packages",
                newName: "Order");

            migrationBuilder.RenameIndex(
                name: "IX_lessonAttachments_LessonId",
                table: "LessonAttachments",
                newName: "IX_LessonAttachments_LessonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LessonAttachments",
                table: "LessonAttachments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonAttachments_Lessons_LessonId",
                table: "LessonAttachments",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonAttachments_Lessons_LessonId",
                table: "LessonAttachments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LessonAttachments",
                table: "LessonAttachments");

            migrationBuilder.RenameTable(
                name: "LessonAttachments",
                newName: "lessonAttachments");

            migrationBuilder.RenameColumn(
                name: "Order",
                table: "Packages",
                newName: "Type");

            migrationBuilder.RenameIndex(
                name: "IX_LessonAttachments_LessonId",
                table: "lessonAttachments",
                newName: "IX_lessonAttachments_LessonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_lessonAttachments",
                table: "lessonAttachments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_lessonAttachments_Lessons_LessonId",
                table: "lessonAttachments",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
