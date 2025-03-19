using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VroomParts.Migrations
{
    /// <inheritdoc />
    public partial class AddedIdToCarPart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "CarParts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarParts",
                table: "CarParts",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CarParts",
                table: "CarParts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CarParts");
        }
    }
}
