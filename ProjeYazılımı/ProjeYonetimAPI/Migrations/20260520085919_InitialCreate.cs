using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjeYonetimAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BütçeKalemleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kategori = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    KategoriIkon = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Tutar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    RenkKod = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SiraNo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BütçeKalemleri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EkipUyeleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdSoyad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Kisaltma = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Uzmanliklar = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AylikUcret = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProjedekiAy = table.Column<int>(type: "int", nullable: false),
                    RenkKod = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    RenkKod2 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EkipUyeleri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HaftalikRaporlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HaftaNo = table.Column<int>(type: "int", nullable: false),
                    HaftaAdi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Tarihler = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Sprint = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TamamlananGorev = table.Column<int>(type: "int", nullable: false),
                    DevamEdenGorev = table.Column<int>(type: "int", nullable: false),
                    Engelleyici = table.Column<int>(type: "int", nullable: false),
                    Verimlilik = table.Column<int>(type: "int", nullable: false),
                    Notlar = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HaftalikRaporlar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjeGorevler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Baslik = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    GorevTuru = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AtananKisi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AtananKisaltma = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    AtananRenk = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Oncelik = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Durum = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Modul = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BaslangicTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GuncellenmeTarihi = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjeGorevler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sprintler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SprintNo = table.Column<int>(type: "int", nullable: false),
                    Baslik = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BaslangicTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Durum = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    HaftaSayisi = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sprintler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SprintGorevler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SprintId = table.Column<int>(type: "int", nullable: false),
                    GorevAdi = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Sorumlu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SorumluKisaltma = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SorumluRenk = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Durum = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SiraNo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SprintGorevler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SprintGorevler_Sprintler_SprintId",
                        column: x => x.SprintId,
                        principalTable: "Sprintler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SprintGorevler_SprintId",
                table: "SprintGorevler",
                column: "SprintId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BütçeKalemleri");

            migrationBuilder.DropTable(
                name: "EkipUyeleri");

            migrationBuilder.DropTable(
                name: "HaftalikRaporlar");

            migrationBuilder.DropTable(
                name: "ProjeGorevler");

            migrationBuilder.DropTable(
                name: "SprintGorevler");

            migrationBuilder.DropTable(
                name: "Sprintler");
        }
    }
}
