using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VroomParts.Migrations
{
    /// <inheritdoc />
    public partial class AddedVehicleCompatibility : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VehicleCompatibility",
                table: "CarParts");

            migrationBuilder.CreateTable(
                name: "VehicleCompatibility",
                columns: table => new
                {
                    VehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CarPartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleCompatibility", x => new { x.CarPartId, x.VehicleId });
                    table.ForeignKey(
                        name: "FK_VehicleCompatibility_CarParts_CarPartId",
                        column: x => x.CarPartId,
                        principalTable: "CarParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VehicleCompatibility_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleCompatibility_VehicleId",
                table: "VehicleCompatibility",
                column: "VehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehicleCompatibility");

            migrationBuilder.AddColumn<string>(
                name: "VehicleCompatibility",
                table: "CarParts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
