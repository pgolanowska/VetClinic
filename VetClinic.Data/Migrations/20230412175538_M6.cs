using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetClinic.Data.Migrations
{
    public partial class M6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmployeePosition",
                table: "Employee",
                newName: "EmployeePositionId");

            migrationBuilder.AddColumn<bool>(
                name: "TitleIsActive",
                table: "Title",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TitleIsActive",
                table: "Title");

            migrationBuilder.RenameColumn(
                name: "EmployeePositionId",
                table: "Employee",
                newName: "EmployeePosition");
        }
    }
}
