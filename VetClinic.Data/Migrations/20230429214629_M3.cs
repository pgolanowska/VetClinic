using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetClinic.Data.Migrations
{
    public partial class M3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ServiceGroupIconName",
                table: "ServiceGroup",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceGroupIconName",
                table: "ServiceGroup");
        }
    }
}
