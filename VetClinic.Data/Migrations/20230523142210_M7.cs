using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetClinic.Data.Migrations
{
    public partial class M7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeServiceGroup",
                columns: table => new
                {
                    EmployeeServiceGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    ServiceGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeServiceGroup", x => x.EmployeeServiceGroupId);
                    table.ForeignKey(
                        name: "FK_EmployeeServiceGroup_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeServiceGroup_ServiceGroup_ServiceGroupId",
                        column: x => x.ServiceGroupId,
                        principalTable: "ServiceGroup",
                        principalColumn: "ServiceGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeServiceGroup_EmployeeId",
                table: "EmployeeServiceGroup",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeServiceGroup_ServiceGroupId",
                table: "EmployeeServiceGroup",
                column: "ServiceGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeServiceGroup");
        }
    }
}
