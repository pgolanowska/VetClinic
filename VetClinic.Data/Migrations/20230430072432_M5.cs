using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetClinic.Data.Migrations
{
    public partial class M5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InfoPage",
                columns: table => new
                {
                    InfoPageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InfoLinkTitle = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    InfoPageTitle = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    InfoContent = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    InfoLastEdited = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoPage", x => x.InfoPageId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InfoPage");
        }
    }
}
