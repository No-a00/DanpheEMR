using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DanpheEMR.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEmployeeTable_V_5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DOB",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Employees",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DOB",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Employees");
        }
    }
}
