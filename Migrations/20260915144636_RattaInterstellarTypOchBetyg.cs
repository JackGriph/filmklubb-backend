using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace filmklubb_backend.Migrations
{
    /// <inheritdoc />
    public partial class RattaInterstellarTypOchBetyg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Rating", "Type" },
                values: new object[] { 5, "Film" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Rating", "Type" },
                values: new object[] { null, "Serie" });
        }
    }
}
