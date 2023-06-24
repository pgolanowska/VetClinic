using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetClinic.Data.Migrations
{
    public partial class M16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavedItem_Employee_EmployeeId",
                table: "SavedItem");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "SavedItem",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AppointmentDateTime",
                table: "SavedItem",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Appointment",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PetId",
                table: "Appointment",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PetSpecies",
                columns: table => new
                {
                    PetSpeciesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PetSpeciesName = table.Column<int>(type: "int", nullable: false),
                    PetSpeciesDescription = table.Column<int>(type: "int", nullable: false),
                    PetSpeciesIsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetSpecies", x => x.PetSpeciesId);
                });

            migrationBuilder.CreateTable(
                name: "Pet",
                columns: table => new
                {
                    PetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PetName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PetSpeciesId = table.Column<int>(type: "int", nullable: false),
                    PetBreed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PetSex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PetDateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PetDistinguishingFeatures = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PetPicture = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PetIsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pet", x => x.PetId);
                    table.ForeignKey(
                        name: "FK_Pet_PetSpecies_PetSpeciesId",
                        column: x => x.PetSpeciesId,
                        principalTable: "PetSpecies",
                        principalColumn: "PetSpeciesId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientPet",
                columns: table => new
                {
                    ClientPetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PetId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientPet", x => x.ClientPetId);
                    table.ForeignKey(
                        name: "FK_ClientPet_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientPet_Pet_PetId",
                        column: x => x.PetId,
                        principalTable: "Pet",
                        principalColumn: "PetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PetHistory",
                columns: table => new
                {
                    PetHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PetId = table.Column<int>(type: "int", nullable: false),
                    PetHistoryTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PetWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PetHistoryNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PetHistoryCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PetHistoryUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetHistory", x => x.PetHistoryId);
                    table.ForeignKey(
                        name: "FK_PetHistory_Pet_PetId",
                        column: x => x.PetId,
                        principalTable: "Pet",
                        principalColumn: "PetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_ClientId",
                table: "Appointment",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_PetId",
                table: "Appointment",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientPet_ClientId",
                table: "ClientPet",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientPet_PetId",
                table: "ClientPet",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_Pet_PetSpeciesId",
                table: "Pet",
                column: "PetSpeciesId");

            migrationBuilder.CreateIndex(
                name: "IX_PetHistory_PetId",
                table: "PetHistory",
                column: "PetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Client_ClientId",
                table: "Appointment",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Pet_PetId",
                table: "Appointment",
                column: "PetId",
                principalTable: "Pet",
                principalColumn: "PetId");

            migrationBuilder.AddForeignKey(
                name: "FK_SavedItem_Employee_EmployeeId",
                table: "SavedItem",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Client_ClientId",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Pet_PetId",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_SavedItem_Employee_EmployeeId",
                table: "SavedItem");

            migrationBuilder.DropTable(
                name: "ClientPet");

            migrationBuilder.DropTable(
                name: "PetHistory");

            migrationBuilder.DropTable(
                name: "Pet");

            migrationBuilder.DropTable(
                name: "PetSpecies");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_ClientId",
                table: "Appointment");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_PetId",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "PetId",
                table: "Appointment");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "SavedItem",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AppointmentDateTime",
                table: "SavedItem",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SavedItem_Employee_EmployeeId",
                table: "SavedItem",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
