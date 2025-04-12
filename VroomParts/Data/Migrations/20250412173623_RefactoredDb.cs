using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VroomParts.Migrations
{
    /// <inheritdoc />
    public partial class RefactoredDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleRecommendations_CarParts_CarPartID",
                table: "VehicleRecommendations");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleRecommendations_Vehicles_CarId",
                table: "VehicleRecommendations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleRecommendations",
                table: "VehicleRecommendations");

            migrationBuilder.DropIndex(
                name: "IX_VehicleRecommendations_CarPartID",
                table: "VehicleRecommendations");

            migrationBuilder.RenameColumn(
                name: "CarPartID",
                table: "VehicleRecommendations",
                newName: "CarPartId");

            migrationBuilder.RenameColumn(
                name: "CarId",
                table: "VehicleRecommendations",
                newName: "VehicleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleRecommendations",
                table: "VehicleRecommendations",
                columns: new[] { "CarPartId", "VehicleId" });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleRecommendations_VehicleId",
                table: "VehicleRecommendations",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleRecommendations_CarParts_CarPartId",
                table: "VehicleRecommendations",
                column: "CarPartId",
                principalTable: "CarParts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleRecommendations_Vehicles_VehicleId",
                table: "VehicleRecommendations",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleRecommendations_CarParts_CarPartId",
                table: "VehicleRecommendations");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleRecommendations_Vehicles_VehicleId",
                table: "VehicleRecommendations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleRecommendations",
                table: "VehicleRecommendations");

            migrationBuilder.DropIndex(
                name: "IX_VehicleRecommendations_VehicleId",
                table: "VehicleRecommendations");

            migrationBuilder.RenameColumn(
                name: "CarPartId",
                table: "VehicleRecommendations",
                newName: "CarPartID");

            migrationBuilder.RenameColumn(
                name: "VehicleId",
                table: "VehicleRecommendations",
                newName: "CarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleRecommendations",
                table: "VehicleRecommendations",
                columns: new[] { "CarId", "CarPartID" });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleRecommendations_CarPartID",
                table: "VehicleRecommendations",
                column: "CarPartID");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleRecommendations_CarParts_CarPartID",
                table: "VehicleRecommendations",
                column: "CarPartID",
                principalTable: "CarParts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleRecommendations_Vehicles_CarId",
                table: "VehicleRecommendations",
                column: "CarId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
