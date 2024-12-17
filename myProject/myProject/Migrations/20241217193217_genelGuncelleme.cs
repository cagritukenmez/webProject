using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myProject.Migrations
{
    /// <inheritdoc />
    public partial class genelGuncelleme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "musteriAd",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "musteriSoyad",
                table: "Randevular");

            migrationBuilder.RenameColumn(
                name: "musteriNumara",
                table: "Randevular",
                newName: "musteriId");

            migrationBuilder.AddColumn<int>(
                name: "PersonellerpersonelID",
                table: "UygunOlmayanTarihSaat",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "personelID",
                table: "UygunOlmayanTarihSaat",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "KullanıcıkullaniciID",
                table: "Randevular",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BerbersalonusalonID",
                table: "Hizmetler",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonelID",
                table: "Hizmetler",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UygunOlmayanTarihSaat_PersonellerpersonelID",
                table: "UygunOlmayanTarihSaat",
                column: "PersonellerpersonelID");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_KullanıcıkullaniciID",
                table: "Randevular",
                column: "KullanıcıkullaniciID");

            migrationBuilder.CreateIndex(
                name: "IX_Hizmetler_BerbersalonusalonID",
                table: "Hizmetler",
                column: "BerbersalonusalonID");

            migrationBuilder.AddForeignKey(
                name: "FK_Hizmetler_BerberSalonları_BerbersalonusalonID",
                table: "Hizmetler",
                column: "BerbersalonusalonID",
                principalTable: "BerberSalonları",
                principalColumn: "salonID");

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Kullanıcı_KullanıcıkullaniciID",
                table: "Randevular",
                column: "KullanıcıkullaniciID",
                principalTable: "Kullanıcı",
                principalColumn: "kullaniciID");

            migrationBuilder.AddForeignKey(
                name: "FK_UygunOlmayanTarihSaat_Personeller_PersonellerpersonelID",
                table: "UygunOlmayanTarihSaat",
                column: "PersonellerpersonelID",
                principalTable: "Personeller",
                principalColumn: "personelID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hizmetler_BerberSalonları_BerbersalonusalonID",
                table: "Hizmetler");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Kullanıcı_KullanıcıkullaniciID",
                table: "Randevular");

            migrationBuilder.DropForeignKey(
                name: "FK_UygunOlmayanTarihSaat_Personeller_PersonellerpersonelID",
                table: "UygunOlmayanTarihSaat");

            migrationBuilder.DropIndex(
                name: "IX_UygunOlmayanTarihSaat_PersonellerpersonelID",
                table: "UygunOlmayanTarihSaat");

            migrationBuilder.DropIndex(
                name: "IX_Randevular_KullanıcıkullaniciID",
                table: "Randevular");

            migrationBuilder.DropIndex(
                name: "IX_Hizmetler_BerbersalonusalonID",
                table: "Hizmetler");

            migrationBuilder.DropColumn(
                name: "PersonellerpersonelID",
                table: "UygunOlmayanTarihSaat");

            migrationBuilder.DropColumn(
                name: "personelID",
                table: "UygunOlmayanTarihSaat");

            migrationBuilder.DropColumn(
                name: "KullanıcıkullaniciID",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "BerbersalonusalonID",
                table: "Hizmetler");

            migrationBuilder.DropColumn(
                name: "PersonelID",
                table: "Hizmetler");

            migrationBuilder.RenameColumn(
                name: "musteriId",
                table: "Randevular",
                newName: "musteriNumara");

            migrationBuilder.AddColumn<string>(
                name: "musteriAd",
                table: "Randevular",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "musteriSoyad",
                table: "Randevular",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");
        }
    }
}
