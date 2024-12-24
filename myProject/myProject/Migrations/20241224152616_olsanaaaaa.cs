using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myProject.Migrations
{
    /// <inheritdoc />
    public partial class olsanaaaaa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hizmetler_BerberSalonları_BerbersalonusalonID",
                table: "Hizmetler");

            migrationBuilder.DropForeignKey(
                name: "FK_Hizmetler_Personeller_PersonellerpersonelID",
                table: "Hizmetler");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Kullanıcı_KullanıcıkullaniciID",
                table: "Randevular");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Personeller_PersonellerpersonelID",
                table: "Randevular");

            migrationBuilder.DropIndex(
                name: "IX_Randevular_KullanıcıkullaniciID",
                table: "Randevular");

            migrationBuilder.DropIndex(
                name: "IX_Randevular_PersonellerpersonelID",
                table: "Randevular");

            migrationBuilder.DropIndex(
                name: "IX_Hizmetler_BerbersalonusalonID",
                table: "Hizmetler");

            migrationBuilder.DropIndex(
                name: "IX_Hizmetler_PersonellerpersonelID",
                table: "Hizmetler");

            migrationBuilder.DropColumn(
                name: "KullanıcıkullaniciID",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "PersonellerpersonelID",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "BerbersalonusalonID",
                table: "Hizmetler");

            migrationBuilder.DropColumn(
                name: "PersonellerpersonelID",
                table: "Hizmetler");

            migrationBuilder.RenameColumn(
                name: "personelId",
                table: "Randevular",
                newName: "personelID");

            migrationBuilder.RenameColumn(
                name: "musteriId",
                table: "Randevular",
                newName: "salonID");

            migrationBuilder.RenameColumn(
                name: "PersonelID",
                table: "Hizmetler",
                newName: "personelID");

            migrationBuilder.AddColumn<int>(
                name: "salonID",
                table: "UygunTarih",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "kullaniciID",
                table: "Randevular",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "salonID",
                table: "Personeller",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "salonID",
                table: "Hizmetler",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UygunTarih_salonID",
                table: "UygunTarih",
                column: "salonID");

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
                name: "IX_Personeller_salonID",
                table: "Personeller",
                column: "salonID");

            migrationBuilder.CreateIndex(
                name: "IX_Hizmetler_personelID",
                table: "Hizmetler",
                column: "personelID");

            migrationBuilder.CreateIndex(
                name: "IX_Hizmetler_salonID",
                table: "Hizmetler",
                column: "salonID");

            migrationBuilder.AddForeignKey(
                name: "FK_Hizmetler_BerberSalonları_salonID",
                table: "Hizmetler",
                column: "salonID",
                principalTable: "BerberSalonları",
                principalColumn: "salonID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Hizmetler_Personeller_personelID",
                table: "Hizmetler",
                column: "personelID",
                principalTable: "Personeller",
                principalColumn: "personelID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Personeller_BerberSalonları_salonID",
                table: "Personeller",
                column: "salonID",
                principalTable: "BerberSalonları",
                principalColumn: "salonID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_BerberSalonları_salonID",
                table: "Randevular",
                column: "salonID",
                principalTable: "BerberSalonları",
                principalColumn: "salonID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Kullanıcı_kullaniciID",
                table: "Randevular",
                column: "kullaniciID",
                principalTable: "Kullanıcı",
                principalColumn: "kullaniciID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Personeller_personelID",
                table: "Randevular",
                column: "personelID",
                principalTable: "Personeller",
                principalColumn: "personelID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UygunTarih_BerberSalonları_salonID",
                table: "UygunTarih",
                column: "salonID",
                principalTable: "BerberSalonları",
                principalColumn: "salonID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hizmetler_BerberSalonları_salonID",
                table: "Hizmetler");

            migrationBuilder.DropForeignKey(
                name: "FK_Hizmetler_Personeller_personelID",
                table: "Hizmetler");

            migrationBuilder.DropForeignKey(
                name: "FK_Personeller_BerberSalonları_salonID",
                table: "Personeller");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_BerberSalonları_salonID",
                table: "Randevular");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Kullanıcı_kullaniciID",
                table: "Randevular");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Personeller_personelID",
                table: "Randevular");

            migrationBuilder.DropForeignKey(
                name: "FK_UygunTarih_BerberSalonları_salonID",
                table: "UygunTarih");

            migrationBuilder.DropIndex(
                name: "IX_UygunTarih_salonID",
                table: "UygunTarih");

            migrationBuilder.DropIndex(
                name: "IX_Randevular_kullaniciID",
                table: "Randevular");

            migrationBuilder.DropIndex(
                name: "IX_Randevular_personelID",
                table: "Randevular");

            migrationBuilder.DropIndex(
                name: "IX_Randevular_salonID",
                table: "Randevular");

            migrationBuilder.DropIndex(
                name: "IX_Personeller_salonID",
                table: "Personeller");

            migrationBuilder.DropIndex(
                name: "IX_Hizmetler_personelID",
                table: "Hizmetler");

            migrationBuilder.DropIndex(
                name: "IX_Hizmetler_salonID",
                table: "Hizmetler");

            migrationBuilder.DropColumn(
                name: "salonID",
                table: "UygunTarih");

            migrationBuilder.DropColumn(
                name: "kullaniciID",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "salonID",
                table: "Personeller");

            migrationBuilder.DropColumn(
                name: "salonID",
                table: "Hizmetler");

            migrationBuilder.RenameColumn(
                name: "personelID",
                table: "Randevular",
                newName: "personelId");

            migrationBuilder.RenameColumn(
                name: "salonID",
                table: "Randevular",
                newName: "musteriId");

            migrationBuilder.RenameColumn(
                name: "personelID",
                table: "Hizmetler",
                newName: "PersonelID");

            migrationBuilder.AddColumn<int>(
                name: "KullanıcıkullaniciID",
                table: "Randevular",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonellerpersonelID",
                table: "Randevular",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BerbersalonusalonID",
                table: "Hizmetler",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonellerpersonelID",
                table: "Hizmetler",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_KullanıcıkullaniciID",
                table: "Randevular",
                column: "KullanıcıkullaniciID");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_PersonellerpersonelID",
                table: "Randevular",
                column: "PersonellerpersonelID");

            migrationBuilder.CreateIndex(
                name: "IX_Hizmetler_BerbersalonusalonID",
                table: "Hizmetler",
                column: "BerbersalonusalonID");

            migrationBuilder.CreateIndex(
                name: "IX_Hizmetler_PersonellerpersonelID",
                table: "Hizmetler",
                column: "PersonellerpersonelID");

            migrationBuilder.AddForeignKey(
                name: "FK_Hizmetler_BerberSalonları_BerbersalonusalonID",
                table: "Hizmetler",
                column: "BerbersalonusalonID",
                principalTable: "BerberSalonları",
                principalColumn: "salonID");

            migrationBuilder.AddForeignKey(
                name: "FK_Hizmetler_Personeller_PersonellerpersonelID",
                table: "Hizmetler",
                column: "PersonellerpersonelID",
                principalTable: "Personeller",
                principalColumn: "personelID");

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Kullanıcı_KullanıcıkullaniciID",
                table: "Randevular",
                column: "KullanıcıkullaniciID",
                principalTable: "Kullanıcı",
                principalColumn: "kullaniciID");

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Personeller_PersonellerpersonelID",
                table: "Randevular",
                column: "PersonellerpersonelID",
                principalTable: "Personeller",
                principalColumn: "personelID");
        }
    }
}
