using Microsoft.EntityFrameworkCore.Migrations;

namespace obg.DataAccess.Migrations
{
    public partial class NewMigration5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Administrators_Pharmacies_PharmacyName",
                table: "Administrators");

            migrationBuilder.DropIndex(
                name: "IX_Administrators_PharmacyName",
                table: "Administrators");

            migrationBuilder.DropColumn(
                name: "PharmacyName",
                table: "Administrators");

            migrationBuilder.AddColumn<string>(
                name: "AdministratorName",
                table: "Pharmacies",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacies_AdministratorName",
                table: "Pharmacies",
                column: "AdministratorName");

            migrationBuilder.AddForeignKey(
                name: "FK_Pharmacies_Administrators_AdministratorName",
                table: "Pharmacies",
                column: "AdministratorName",
                principalTable: "Administrators",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pharmacies_Administrators_AdministratorName",
                table: "Pharmacies");

            migrationBuilder.DropIndex(
                name: "IX_Pharmacies_AdministratorName",
                table: "Pharmacies");

            migrationBuilder.DropColumn(
                name: "AdministratorName",
                table: "Pharmacies");

            migrationBuilder.AddColumn<string>(
                name: "PharmacyName",
                table: "Administrators",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Administrators_PharmacyName",
                table: "Administrators",
                column: "PharmacyName");

            migrationBuilder.AddForeignKey(
                name: "FK_Administrators_Pharmacies_PharmacyName",
                table: "Administrators",
                column: "PharmacyName",
                principalTable: "Pharmacies",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
