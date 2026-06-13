using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OtoparkSistemi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Araclar",
                columns: table => new
                {
                    AracId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Plaka = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    AracTipi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SahibiAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SahibiTelefon = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    OlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Araclar", x => x.AracId);
                });

            migrationBuilder.CreateTable(
                name: "Tarifeler",
                columns: table => new
                {
                    TarifeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AracTipi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SaatlikUcret = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GunlukMaksimum = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GecerlilikBaslangic = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GecerlilikBitis = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarifeler", x => x.TarifeId);
                });

            migrationBuilder.CreateTable(
                name: "Abonelikler",
                columns: table => new
                {
                    AbonelikId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AracId = table.Column<int>(type: "int", nullable: false),
                    BaslangicTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AbonelikTipi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ucret = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AktifMi = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abonelikler", x => x.AbonelikId);
                    table.ForeignKey(
                        name: "FK_Abonelikler_Araclar_AracId",
                        column: x => x.AracId,
                        principalTable: "Araclar",
                        principalColumn: "AracId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GirisKayitlari",
                columns: table => new
                {
                    KayitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AracId = table.Column<int>(type: "int", nullable: false),
                    GirisTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CikisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ToplamSure = table.Column<int>(type: "int", nullable: true),
                    OdenenUcret = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AbonelikKullanildiMi = table.Column<bool>(type: "bit", nullable: false),
                    OdemeDurumu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GirisKayitlari", x => x.KayitId);
                    table.ForeignKey(
                        name: "FK_GirisKayitlari_Araclar_AracId",
                        column: x => x.AracId,
                        principalTable: "Araclar",
                        principalColumn: "AracId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Abonelikler_AracId",
                table: "Abonelikler",
                column: "AracId");

            migrationBuilder.CreateIndex(
                name: "IX_Araclar_Plaka",
                table: "Araclar",
                column: "Plaka",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GirisKayitlari_AracId",
                table: "GirisKayitlari",
                column: "AracId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abonelikler");

            migrationBuilder.DropTable(
                name: "GirisKayitlari");

            migrationBuilder.DropTable(
                name: "Tarifeler");

            migrationBuilder.DropTable(
                name: "Araclar");
        }
    }
}
