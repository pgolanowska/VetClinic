#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace VetClinic.Data.Migrations
{
    public partial class M1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    NewsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LinkTitle = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PageTitle = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Content = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewsIsNotArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.NewsId);
                });

            migrationBuilder.CreateTable(
                name: "Position",
                columns: table => new
                {
                    PositionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PositionName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PositionIsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position", x => x.PositionId);
                });

            migrationBuilder.CreateTable(
                name: "ServiceGroup",
                columns: table => new
                {
                    ServiceGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceGroupName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ServiceGroupDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceGroupIsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceGroup", x => x.ServiceGroupId);
                });

            migrationBuilder.CreateTable(
                name: "Title",
                columns: table => new
                {
                    TitleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TitleIsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Title", x => x.TitleId);
                });

            migrationBuilder.CreateTable(
                name: "PositionServiceGroup",
                columns: table => new
                {
                    PositionsPositionId = table.Column<int>(type: "int", nullable: false),
                    ServiceGroupsServiceGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PositionServiceGroup", x => new { x.PositionsPositionId, x.ServiceGroupsServiceGroupId });
                    table.ForeignKey(
                        name: "FK_PositionServiceGroup_Position_PositionsPositionId",
                        column: x => x.PositionsPositionId,
                        principalTable: "Position",
                        principalColumn: "PositionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PositionServiceGroup_ServiceGroup_ServiceGroupsServiceGroupId",
                        column: x => x.ServiceGroupsServiceGroupId,
                        principalTable: "ServiceGroup",
                        principalColumn: "ServiceGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EmployeeSurname = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    EmployeeTitle = table.Column<int>(type: "int", nullable: false),
                    TitleId = table.Column<int>(type: "int", nullable: false),
                    EmployeePosition = table.Column<int>(type: "int", nullable: false),
                    PositionId = table.Column<int>(type: "int", nullable: false),
                    EmployeeEducation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EmployeeBio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeePhotoURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeIsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employee_Position_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Position",
                        principalColumn: "PositionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employee_Title_TitleId",
                        column: x => x.TitleId,
                        principalTable: "Title",
                        principalColumn: "TitleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_PositionId",
                table: "Employee",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_TitleId",
                table: "Employee",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "IX_PositionServiceGroup_ServiceGroupsServiceGroupId",
                table: "PositionServiceGroup",
                column: "ServiceGroupsServiceGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "PositionServiceGroup");

            migrationBuilder.DropTable(
                name: "Title");

            migrationBuilder.DropTable(
                name: "Position");

            migrationBuilder.DropTable(
                name: "ServiceGroup");
        }
    }
}
