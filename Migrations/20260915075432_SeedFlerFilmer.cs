using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace filmklubb_backend.Migrations
{
    /// <inheritdoc />
    public partial class SeedFlerFilmer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "CreatedAt", "ImageUrl", "Notes", "Rating", "Title", "Type", "Watched", "WatchedAt" },
                values: new object[,]
                {
                    { 5, new DateTime(2026, 2, 9, 8, 15, 0, 0, DateTimeKind.Utc), null, "Grym story, ännu bättre score", null, "Interstellar", "Serie", true, null },
                    { 6, new DateTime(2026, 3, 2, 19, 30, 0, 0, DateTimeKind.Utc), null, "Spara till en mörk kväll.", null, "Nosferatu", "Film", false, null },
                    { 7, new DateTime(2026, 3, 14, 11, 0, 0, 0, DateTimeKind.Utc), null, "Stressig på ett bra sätt. Kolla inte på tom mage.", 5, "The Bear", "Serie", true, new DateTime(2026, 4, 1, 22, 15, 0, 0, DateTimeKind.Utc) },
                    { 8, new DateTime(2026, 4, 8, 16, 45, 0, 0, DateTimeKind.Utc), null, null, 4, "Past Lives", "Film", true, new DateTime(2026, 4, 20, 20, 0, 0, 0, DateTimeKind.Utc) },
                    { 9, new DateTime(2026, 5, 3, 9, 0, 0, 0, DateTimeKind.Utc), null, "Sägs vara bra även om man skiter i Star Wars.", null, "Andor", "Serie", false, null },
                    { 10, new DateTime(2026, 5, 22, 13, 20, 0, 0, DateTimeKind.Utc), null, "Snyggast i år, men lite för lång.", 3, "Poor Things", "Film", true, new DateTime(2026, 6, 2, 21, 30, 0, 0, DateTimeKind.Utc) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
