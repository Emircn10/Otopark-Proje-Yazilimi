using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjeYonetimAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddGorevDates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BaslangicTarihi",
                table: "SprintGorevler",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BitisTarihi",
                table: "SprintGorevler",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaslangicTarihi",
                table: "SprintGorevler");

            migrationBuilder.DropColumn(
                name: "BitisTarihi",
                table: "SprintGorevler");
        }
    }
}
