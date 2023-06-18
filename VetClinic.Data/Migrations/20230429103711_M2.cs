using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetClinic.Data.Migrations
{
    public partial class M2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeePhotoURL",
                table: "Employee");

            migrationBuilder.AddColumn<byte[]>(
                name: "EmployeePhoto",
                table: "Employee",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeePhoto",
                table: "Employee");

            migrationBuilder.AddColumn<string>(
                name: "EmployeePhotoURL",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
