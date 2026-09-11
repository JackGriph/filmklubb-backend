using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace filmklubb_backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Type = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Watched = table.Column<bool>(type: "INTEGER", nullable: false),
                    Rating = table.Column<int>(type: "INTEGER", nullable: true),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    WatchedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "CreatedAt", "ImageUrl", "Notes", "Rating", "Title", "Type", "Watched", "WatchedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 10, 18, 0, 0, 0, DateTimeKind.Utc), null, "Bäst på stor duk. Ljudmixen bär hela filmen.", 5, "Dune: Part Two", "Film", true, new DateTime(2026, 1, 18, 20, 30, 0, 0, DateTimeKind.Utc) },
                    { 2, new DateTime(2026, 1, 12, 9, 15, 0, 0, DateTimeKind.Utc), null, "Säsong 1 håller hela vägen. Långsam start.", 4, "Severance", "Serie", true, new DateTime(2026, 2, 2, 21, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, new DateTime(2026, 2, 5, 12, 0, 0, 0, DateTimeKind.Utc), null, "Tre och en halv timme - boka in en hel kväll.", null, "The Brutalist", "Film", false, null },
                    { 4, new DateTime(2026, 2, 20, 8, 45, 0, 0, DateTimeKind.Utc), null, null, null, "Shogun", "Serie", false, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
