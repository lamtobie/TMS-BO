using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS_FinalProject.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "services",
                table: "employee",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "services",
                table: "employee");
        }
    }
}
