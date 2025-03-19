using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VroomParts.Migrations
{
    /// <inheritdoc />
    public partial class AddedCategoryEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "CarParts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "CarParts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CategoryId", "ImageUrl" },
                values: new object[] { null, "/images/radio_astra_h_2.jpg" });

            migrationBuilder.UpdateData(
                table: "CarParts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CategoryId",
                value: null);

            migrationBuilder.UpdateData(
                table: "CarParts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CategoryId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_CarParts_CategoryId",
                table: "CarParts",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarParts_Categories_CategoryId",
                table: "CarParts",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarParts_Categories_CategoryId",
                table: "CarParts");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_CarParts_CategoryId",
                table: "CarParts");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "CarParts");

            migrationBuilder.UpdateData(
                table: "CarParts",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "/images/radio_astra_h_1.jpg");
        }
    }
}
