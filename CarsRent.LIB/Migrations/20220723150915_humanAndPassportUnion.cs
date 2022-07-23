using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarsRent.LIB.Migrations
{
    public partial class humanAndPassportUnion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Passports");

            migrationBuilder.AddColumn<string>(
                name: "IdentityNumber",
                table: "Humans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IssuingDate",
                table: "Humans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IssuingOrganization",
                table: "Humans",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RegistrationPlace",
                table: "Humans",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdentityNumber",
                table: "Humans");

            migrationBuilder.DropColumn(
                name: "IssuingDate",
                table: "Humans");

            migrationBuilder.DropColumn(
                name: "IssuingOrganization",
                table: "Humans");

            migrationBuilder.DropColumn(
                name: "RegistrationPlace",
                table: "Humans");

            migrationBuilder.CreateTable(
                name: "Passports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HumanId = table.Column<int>(type: "int", nullable: true),
                    IdentityNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IssuingDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IssuingOrganization = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RegistrationPlace = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Passports_Humans_HumanId",
                        column: x => x.HumanId,
                        principalTable: "Humans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Passports_HumanId",
                table: "Passports",
                column: "HumanId",
                unique: true,
                filter: "[HumanId] IS NOT NULL");
        }
    }
}
