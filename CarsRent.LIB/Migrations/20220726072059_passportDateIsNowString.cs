using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarsRent.LIB.Migrations
{
    public partial class passportDateIsNowString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PassportIssuingDate",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PassportIssuingDate",
                table: "Cars",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
