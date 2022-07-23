using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarsRent.LIB.Migrations
{
    public partial class humanPassportFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Humans_Passports_PassportId",
                table: "Humans");

            migrationBuilder.DropIndex(
                name: "IX_Humans_PassportId",
                table: "Humans");

            migrationBuilder.DropColumn(
                name: "PassportId",
                table: "Humans");

            migrationBuilder.AddColumn<int>(
                name: "HumanId",
                table: "Passports",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Passports_HumanId",
                table: "Passports",
                column: "HumanId",
                unique: true,
                filter: "[HumanId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Passports_Humans_HumanId",
                table: "Passports",
                column: "HumanId",
                principalTable: "Humans",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Passports_Humans_HumanId",
                table: "Passports");

            migrationBuilder.DropIndex(
                name: "IX_Passports_HumanId",
                table: "Passports");

            migrationBuilder.DropColumn(
                name: "HumanId",
                table: "Passports");

            migrationBuilder.AddColumn<int>(
                name: "PassportId",
                table: "Humans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Humans_PassportId",
                table: "Humans",
                column: "PassportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Humans_Passports_PassportId",
                table: "Humans",
                column: "PassportId",
                principalTable: "Passports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
