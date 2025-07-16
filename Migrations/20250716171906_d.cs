using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UTS_Project_Efengsi_Rahmanto_Zalukhu.Migrations
{
    /// <inheritdoc />
    public partial class d : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Pesanans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NamePembeli = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AlamatPembeli = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdProduk = table.Column<int>(type: "int", nullable: false),
                    JumlaH = table.Column<int>(type: "int", nullable: false),
                    TanggalPesanan = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pesanans", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Produks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NameProduk = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Supplier = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TanggalKadalwarsa = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Harga = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ImagePath = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produks", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Produks",
                columns: new[] { "Id", "CreateDate", "Harga", "ImagePath", "NameProduk", "Supplier", "TanggalKadalwarsa" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 17, 0, 19, 6, 151, DateTimeKind.Local).AddTicks(2558), 100000m, "Images/Produk/aa.jpg", "Produk Sample 1", "Supplier A", new DateTime(2026, 7, 17, 0, 19, 6, 151, DateTimeKind.Local).AddTicks(2540) },
                    { 2, new DateTime(2025, 7, 17, 0, 19, 6, 151, DateTimeKind.Local).AddTicks(2564), 150000m, "Images/Produk/aa.jpg", "Produk Sample 2", "Supplier B", new DateTime(2027, 7, 17, 0, 19, 6, 151, DateTimeKind.Local).AddTicks(2562) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pesanans");

            migrationBuilder.DropTable(
                name: "Produks");
        }
    }
}
