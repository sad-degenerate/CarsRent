using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarsRent.LIB.Migrations
{
    public partial class addCarStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarStatus",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarStatus",
                table: "Cars");
        }
    }
}
