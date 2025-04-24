using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VroomParts.Migrations
{
    /// <inheritdoc />
    public partial class AddedViewedCarPartTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ViewedCarParts",
                columns: table => new
                {
                    CarPartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ViewCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewedCarParts", x => new { x.CarPartId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ViewedCarParts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ViewedCarParts_CarParts_CarPartId",
                        column: x => x.CarPartId,
                        principalTable: "CarParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ViewedCarParts_UserId",
                table: "ViewedCarParts",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ViewedCarParts");
        }
    }
}
