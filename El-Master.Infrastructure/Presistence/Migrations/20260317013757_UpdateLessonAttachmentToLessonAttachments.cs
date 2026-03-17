using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace El_Master.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLessonAttachmentToLessonAttachments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonAttachment_Lessons_LessonId",
                table: "LessonAttachment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LessonAttachment",
                table: "LessonAttachment");

            migrationBuilder.RenameTable(
                name: "LessonAttachment",
                newName: "lessonAttachments");

            migrationBuilder.RenameIndex(
                name: "IX_LessonAttachment_LessonId",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lessonAttachments_Lessons_LessonId",
                table: "lessonAttachments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_lessonAttachments",
                table: "lessonAttachments");

            migrationBuilder.RenameTable(
                name: "lessonAttachments",
                newName: "LessonAttachment");

            migrationBuilder.RenameIndex(
                name: "IX_lessonAttachments_LessonId",
                table: "LessonAttachment",
                newName: "IX_LessonAttachment_LessonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LessonAttachment",
                table: "LessonAttachment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonAttachment_Lessons_LessonId",
                table: "LessonAttachment",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
