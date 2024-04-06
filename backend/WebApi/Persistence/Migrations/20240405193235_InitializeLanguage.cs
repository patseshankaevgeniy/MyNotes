using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitializeLanguage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Languages",
                columns: new[] { "Id", "Value" },
                values: new object[,]
                {
                    { new Guid("d49d53cf-adb3-4507-b7c9-81bec78db9b6"), "ru" },
                    { new Guid("d49d53cf-adb3-4507-b7c9-81bec78db9b9"), "eng" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("d49d53cf-adb3-4507-b7c9-81bec78db9b6"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Languages",
                keyColumn: "Id",
                keyValue: new Guid("d49d53cf-adb3-4507-b7c9-81bec78db9b9"));
        }
    }
}
