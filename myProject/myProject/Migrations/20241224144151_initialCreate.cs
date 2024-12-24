using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myProject.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate : Migration
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
                name: "Kullanıcı",
                columns: table => new
                {
                    kullaniciID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    kullaniciAdi = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    kullaniciSifre = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    eposta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    telNo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    rol = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanıcı", x => x.kullaniciID);
                });

            migrationBuilder.CreateTable(
                name: "Personeller",
                columns: table => new
                {
                    personelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    isim = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    soyisim = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    eposta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    telNo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personeller", x => x.personelID);
                });

            migrationBuilder.CreateTable(
                name: "UygunTarih",
                columns: table => new
                {
                    tarihID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UygunTarih", x => x.tarihID);
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
                    PersonelID = table.Column<int>(type: "int", nullable: false),
                    BerbersalonusalonID = table.Column<int>(type: "int", nullable: true),
                    PersonellerpersonelID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hizmetler", x => x.hizmetID);
                    table.ForeignKey(
                        name: "FK_Hizmetler_BerberSalonları_BerbersalonusalonID",
                        column: x => x.BerbersalonusalonID,
                        principalTable: "BerberSalonları",
                        principalColumn: "salonID");
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
                    randevuTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    randevuSaati = table.Column<TimeSpan>(type: "time", nullable: false),
                    musteriId = table.Column<int>(type: "int", nullable: false),
                    personelId = table.Column<int>(type: "int", nullable: false),
                    KullanıcıkullaniciID = table.Column<int>(type: "int", nullable: true),
                    PersonellerpersonelID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Randevular", x => x.randevuID);
                    table.ForeignKey(
                        name: "FK_Randevular_Kullanıcı_KullanıcıkullaniciID",
                        column: x => x.KullanıcıkullaniciID,
                        principalTable: "Kullanıcı",
                        principalColumn: "kullaniciID");
                    table.ForeignKey(
                        name: "FK_Randevular_Personeller_PersonellerpersonelID",
                        column: x => x.PersonellerpersonelID,
                        principalTable: "Personeller",
                        principalColumn: "personelID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hizmetler_BerbersalonusalonID",
                table: "Hizmetler",
                column: "BerbersalonusalonID");

            migrationBuilder.CreateIndex(
                name: "IX_Hizmetler_PersonellerpersonelID",
                table: "Hizmetler",
                column: "PersonellerpersonelID");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_KullanıcıkullaniciID",
                table: "Randevular",
                column: "KullanıcıkullaniciID");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_PersonellerpersonelID",
                table: "Randevular",
                column: "PersonellerpersonelID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hizmetler");

            migrationBuilder.DropTable(
                name: "Randevular");

            migrationBuilder.DropTable(
                name: "UygunTarih");

            migrationBuilder.DropTable(
                name: "BerberSalonları");

            migrationBuilder.DropTable(
                name: "Kullanıcı");

            migrationBuilder.DropTable(
                name: "Personeller");
        }
    }
}
