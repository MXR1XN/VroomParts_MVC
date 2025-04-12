using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VroomParts.Migrations
{
    /// <inheritdoc />
    public partial class VehicleRecomendation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Make = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleRecommendations",
                columns: table => new
                {
                    CarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CarPartID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleRecommendations", x => new { x.CarId, x.CarPartID });
                    table.ForeignKey(
                        name: "FK_VehicleRecommendations_CarParts_CarPartID",
                        column: x => x.CarPartID,
                        principalTable: "CarParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VehicleRecommendations_Vehicles_CarId",
                        column: x => x.CarId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleRecommendations_CarPartID",
                table: "VehicleRecommendations",
                column: "CarPartID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehicleRecommendations");

            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
