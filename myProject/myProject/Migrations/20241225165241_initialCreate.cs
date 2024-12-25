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
                    salonNumara = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
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
                    telNo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    salonID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personeller", x => x.personelID);
                    table.ForeignKey(
                        name: "FK_Personeller_BerberSalonları_salonID",
                        column: x => x.salonID,
                        principalTable: "BerberSalonları",
                        principalColumn: "salonID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UygunTarih",
                columns: table => new
                {
                    tarihID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    salonID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UygunTarih", x => x.tarihID);
                    table.ForeignKey(
                        name: "FK_UygunTarih_BerberSalonları_salonID",
                        column: x => x.salonID,
                        principalTable: "BerberSalonları",
                        principalColumn: "salonID",
                        onDelete: ReferentialAction.Restrict);
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
                    personelID = table.Column<int>(type: "int", nullable: false),
                    salonID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hizmetler", x => x.hizmetID);
                    table.ForeignKey(
                        name: "FK_Hizmetler_BerberSalonları_salonID",
                        column: x => x.salonID,
                        principalTable: "BerberSalonları",
                        principalColumn: "salonID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Hizmetler_Personeller_personelID",
                        column: x => x.personelID,
                        principalTable: "Personeller",
                        principalColumn: "personelID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Randevular",
                columns: table => new
                {
                    randevuID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    randevuTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    randevuSaati = table.Column<TimeSpan>(type: "time", nullable: false),
                    kullaniciID = table.Column<int>(type: "int", nullable: false),
                    personelID = table.Column<int>(type: "int", nullable: false),
                    salonID = table.Column<int>(type: "int", nullable: false),
                    hizmetID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Randevular", x => x.randevuID);
                    table.ForeignKey(
                        name: "FK_Randevular_BerberSalonları_salonID",
                        column: x => x.salonID,
                        principalTable: "BerberSalonları",
                        principalColumn: "salonID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Randevular_Hizmetler_hizmetID",
                        column: x => x.hizmetID,
                        principalTable: "Hizmetler",
                        principalColumn: "hizmetID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Randevular_Kullanıcı_kullaniciID",
                        column: x => x.kullaniciID,
                        principalTable: "Kullanıcı",
                        principalColumn: "kullaniciID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Randevular_Personeller_personelID",
                        column: x => x.personelID,
                        principalTable: "Personeller",
                        principalColumn: "personelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hizmetler_personelID",
                table: "Hizmetler",
                column: "personelID");

            migrationBuilder.CreateIndex(
                name: "IX_Hizmetler_salonID",
                table: "Hizmetler",
                column: "salonID");

            migrationBuilder.CreateIndex(
                name: "IX_Personeller_salonID",
                table: "Personeller",
                column: "salonID");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_hizmetID",
                table: "Randevular",
                column: "hizmetID");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_kullaniciID",
                table: "Randevular",
                column: "kullaniciID");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_personelID",
                table: "Randevular",
                column: "personelID");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_salonID",
                table: "Randevular",
                column: "salonID");

            migrationBuilder.CreateIndex(
                name: "IX_UygunTarih_salonID",
                table: "UygunTarih",
                column: "salonID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Randevular");

            migrationBuilder.DropTable(
                name: "UygunTarih");

            migrationBuilder.DropTable(
                name: "Hizmetler");

            migrationBuilder.DropTable(
                name: "Kullanıcı");

            migrationBuilder.DropTable(
                name: "Personeller");

            migrationBuilder.DropTable(
                name: "BerberSalonları");
        }
    }
}
