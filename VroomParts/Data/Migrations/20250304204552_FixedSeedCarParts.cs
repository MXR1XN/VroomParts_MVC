using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VroomParts.Migrations
{
    /// <inheritdoc />
    public partial class FixedSeedCarParts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarParts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleCompatibility = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarParts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CarParts",
                columns: new[] { "Id", "DateAdded", "Description", "ImageUrl", "Name", "Price", "VehicleCompatibility" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 5, 1, 11, 0, 0, 0, DateTimeKind.Unspecified), "High-performance brake pads for sport cars.", "", "Brake Pads", 59.99m, "BMW M3, Audi A4, Mercedes C-Class" },
                    { 2, new DateTime(2024, 4, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), "Long-lasting air filter for better engine efficiency.", "", "Air Filter", 29.99m, "Toyota Corolla, Honda Civic, Ford Focus" },
                    { 3, new DateTime(2024, 3, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), "Platinum spark plugs for improved fuel efficiency.", "", "Spark Plugs", 15.99m, "Ford Mustang, Chevrolet Camaro, Dodge Charger" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarParts");
        }
    }
}
