using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myProject.Migrations
{
    /// <inheritdoc />
    public partial class hey333 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hizmetler_Personeller_PersonellerpersonelID",
                table: "Hizmetler");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Kullanıcı_KullanıcıkullaniciID",
                table: "Randevular");

            migrationBuilder.DropIndex(
                name: "IX_Hizmetler_PersonellerpersonelID",
                table: "Hizmetler");

            migrationBuilder.DropColumn(
                name: "PersonellerpersonelID",
                table: "Hizmetler");

            migrationBuilder.RenameColumn(
                name: "KullanıcıkullaniciID",
                table: "Randevular",
                newName: "kullanıcıkullaniciID");

            migrationBuilder.RenameIndex(
                name: "IX_Randevular_KullanıcıkullaniciID",
                table: "Randevular",
                newName: "IX_Randevular_kullanıcıkullaniciID");

            migrationBuilder.AlterColumn<int>(
                name: "kullanıcıkullaniciID",
                table: "Randevular",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hizmetler_PersonelID",
                table: "Hizmetler",
                column: "PersonelID");

            migrationBuilder.AddForeignKey(
                name: "FK_Hizmetler_Personeller_PersonelID",
                table: "Hizmetler",
                column: "PersonelID",
                principalTable: "Personeller",
                principalColumn: "personelID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Kullanıcı_kullanıcıkullaniciID",
                table: "Randevular",
                column: "kullanıcıkullaniciID",
                principalTable: "Kullanıcı",
                principalColumn: "kullaniciID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hizmetler_Personeller_PersonelID",
                table: "Hizmetler");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Kullanıcı_kullanıcıkullaniciID",
                table: "Randevular");

            migrationBuilder.DropIndex(
                name: "IX_Hizmetler_PersonelID",
                table: "Hizmetler");

            migrationBuilder.RenameColumn(
                name: "kullanıcıkullaniciID",
                table: "Randevular",
                newName: "KullanıcıkullaniciID");

            migrationBuilder.RenameIndex(
                name: "IX_Randevular_kullanıcıkullaniciID",
                table: "Randevular",
                newName: "IX_Randevular_KullanıcıkullaniciID");

            migrationBuilder.AlterColumn<int>(
                name: "KullanıcıkullaniciID",
                table: "Randevular",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PersonellerpersonelID",
                table: "Hizmetler",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hizmetler_PersonellerpersonelID",
                table: "Hizmetler",
                column: "PersonellerpersonelID");

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
        }
    }
}
