using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OtoparkSistemi.Migrations
{
    /// <inheritdoc />
    public partial class AddTieredPricing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SaatlikUcret",
                table: "Tarifeler",
                newName: "Saat3PlusSaatlik");

            migrationBuilder.AddColumn<decimal>(
                name: "Ilk1SaatUcret",
                table: "Tarifeler",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Saat1_3Ucret",
                table: "Tarifeler",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "ZiyaretciMi",
                table: "GirisKayitlari",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ilk1SaatUcret",
                table: "Tarifeler");

            migrationBuilder.DropColumn(
                name: "Saat1_3Ucret",
                table: "Tarifeler");

            migrationBuilder.DropColumn(
                name: "ZiyaretciMi",
                table: "GirisKayitlari");

            migrationBuilder.RenameColumn(
                name: "Saat3PlusSaatlik",
                table: "Tarifeler",
                newName: "SaatlikUcret");
        }
    }
}
