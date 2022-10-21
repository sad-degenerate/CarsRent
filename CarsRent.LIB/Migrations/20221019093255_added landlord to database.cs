using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarsRent.LIB.Migrations
{
    public partial class addedlandlordtodatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LandlordId",
                table: "Cars",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LandlordInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LandlordId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LandlordInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LandlordInfos_Humans_LandlordId",
                        column: x => x.LandlordId,
                        principalTable: "Humans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RenterInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RenterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RenterInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RenterInfos_Humans_RenterId",
                        column: x => x.RenterId,
                        principalTable: "Humans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_LandlordId",
                table: "Cars",
                column: "LandlordId");

            migrationBuilder.CreateIndex(
                name: "IX_LandlordInfos_LandlordId",
                table: "LandlordInfos",
                column: "LandlordId");

            migrationBuilder.CreateIndex(
                name: "IX_RenterInfos_RenterId",
                table: "RenterInfos",
                column: "RenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_LandlordInfos_LandlordId",
                table: "Cars",
                column: "LandlordId",
                principalTable: "LandlordInfos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_LandlordInfos_LandlordId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "LandlordInfos");

            migrationBuilder.DropTable(
                name: "RenterInfos");

            migrationBuilder.DropIndex(
                name: "IX_Cars_LandlordId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "LandlordId",
                table: "Cars");
        }
    }
}
