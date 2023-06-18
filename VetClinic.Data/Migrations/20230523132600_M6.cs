using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetClinic.Data.Migrations
{
    public partial class M6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    ServiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ServiceDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceIsActive = table.Column<bool>(type: "bit", nullable: false),
                    ServiceGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.ServiceId);
                    table.ForeignKey(
                        name: "FK_Service_ServiceGroup_ServiceGroupId",
                        column: x => x.ServiceGroupId,
                        principalTable: "ServiceGroup",
                        principalColumn: "ServiceGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Service_ServiceGroupId",
                table: "Service",
                column: "ServiceGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Service");
        }
    }
}
