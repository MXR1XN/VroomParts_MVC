using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VroomParts.Migrations
{
    /// <inheritdoc />
    public partial class AddCarPartImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CarParts",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "/images/radio_astra_h_1.jpg");

            migrationBuilder.UpdateData(
                table: "CarParts",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "/images/radio_astra_h_2.jpg");

            migrationBuilder.UpdateData(
                table: "CarParts",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "/images/radio_astra_h_3.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CarParts",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "");

            migrationBuilder.UpdateData(
                table: "CarParts",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "");

            migrationBuilder.UpdateData(
                table: "CarParts",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "");
        }
    }
}
