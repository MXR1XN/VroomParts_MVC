using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VroomParts.Migrations
{
    /// <inheritdoc />
    public partial class DropIdFromCarPart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CarParts",
                table: "CarParts");

            migrationBuilder.DeleteData(
                table: "CarParts",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CarParts",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CarParts",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CarParts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CarParts",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarParts",
                table: "CarParts",
                column: "Id");

            migrationBuilder.InsertData(
                table: "CarParts",
                columns: new[] { "Id", "CategoryId", "DateAdded", "Description", "ImageUrl", "Name", "Price", "VehicleCompatibility" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2024, 5, 1, 11, 0, 0, 0, DateTimeKind.Unspecified), "High-performance brake pads for sport cars.", "/images/radio_astra_h_2.jpg", "Brake Pads", 59.99m, "BMW M3, Audi A4, Mercedes C-Class" },
                    { 2, null, new DateTime(2024, 4, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), "Long-lasting air filter for better engine efficiency.", "/images/radio_astra_h_2.jpg", "Air Filter", 29.99m, "Toyota Corolla, Honda Civic, Ford Focus" },
                    { 3, null, new DateTime(2024, 3, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), "Platinum spark plugs for improved fuel efficiency.", "/images/radio_astra_h_3.jpg", "Spark Plugs", 15.99m, "Ford Mustang, Chevrolet Camaro, Dodge Charger" }
                });
        }
    }
}
