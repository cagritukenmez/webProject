using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myProject.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BerberSalonları",
                columns: table => new
                {
                    salonID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    salonAd = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    salonAdres = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    salonNumara = table.Column<int>(type: "int", nullable: false),
                    salonEposta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    baslangicSaati = table.Column<TimeOnly>(type: "time", nullable: false),
                    bitisSaati = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BerberSalonları", x => x.salonID);
                });

            migrationBuilder.CreateTable(
                name: "Personeller",
                columns: table => new
                {
                    personelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    isim = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    soyisim = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    BerbersalonusalonID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personeller", x => x.personelID);
                    table.ForeignKey(
                        name: "FK_Personeller_BerberSalonları_BerbersalonusalonID",
                        column: x => x.BerbersalonusalonID,
                        principalTable: "BerberSalonları",
                        principalColumn: "salonID");
                });

            migrationBuilder.CreateTable(
                name: "Hizmetler",
                columns: table => new
                {
                    hizmetID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    hizmetAdi = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    hizmetSuresi = table.Column<TimeOnly>(type: "time", nullable: false),
                    hizmetFiyat = table.Column<int>(type: "int", nullable: false),
                    PersonellerpersonelID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hizmetler", x => x.hizmetID);
                    table.ForeignKey(
                        name: "FK_Hizmetler_Personeller_PersonellerpersonelID",
                        column: x => x.PersonellerpersonelID,
                        principalTable: "Personeller",
                        principalColumn: "personelID");
                });

            migrationBuilder.CreateTable(
                name: "Randevular",
                columns: table => new
                {
                    randevuID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    musteriNumara = table.Column<int>(type: "int", nullable: false),
                    randevuTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    randevuSaati = table.Column<TimeSpan>(type: "time", nullable: false),
                    musteriAd = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    musteriSoyad = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    personelID = table.Column<int>(type: "int", nullable: false),
                    hizmetID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Randevular", x => x.randevuID);
                    table.ForeignKey(
                        name: "FK_Randevular_Hizmetler_hizmetID",
                        column: x => x.hizmetID,
                        principalTable: "Hizmetler",
                        principalColumn: "hizmetID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Randevular_Personeller_personelID",
                        column: x => x.personelID,
                        principalTable: "Personeller",
                        principalColumn: "personelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hizmetler_PersonellerpersonelID",
                table: "Hizmetler",
                column: "PersonellerpersonelID");

            migrationBuilder.CreateIndex(
                name: "IX_Personeller_BerbersalonusalonID",
                table: "Personeller",
                column: "BerbersalonusalonID");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_hizmetID",
                table: "Randevular",
                column: "hizmetID");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_personelID",
                table: "Randevular",
                column: "personelID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Randevular");

            migrationBuilder.DropTable(
                name: "Hizmetler");

            migrationBuilder.DropTable(
                name: "Personeller");

            migrationBuilder.DropTable(
                name: "BerberSalonları");
        }
    }
}
