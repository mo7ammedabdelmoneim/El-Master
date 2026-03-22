using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace El_Master.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePackageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Packages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Packages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Packages");
        }
    }
}
