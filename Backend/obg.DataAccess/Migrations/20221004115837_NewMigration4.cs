using Microsoft.EntityFrameworkCore.Migrations;

namespace obg.DataAccess.Migrations
{
    public partial class NewMigration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Demands_Employees_EmployeeName",
                table: "Demands");

            migrationBuilder.DropForeignKey(
                name: "FK_Pharmacies_Administrators_AdministratorName",
                table: "Pharmacies");

            migrationBuilder.DropForeignKey(
                name: "FK_Pharmacies_Owners_OwnerName",
                table: "Pharmacies");

            migrationBuilder.DropIndex(
                name: "IX_Pharmacies_AdministratorName",
                table: "Pharmacies");

            migrationBuilder.DropIndex(
                name: "IX_Pharmacies_OwnerName",
                table: "Pharmacies");

            migrationBuilder.DropColumn(
                name: "AdministratorName",
                table: "Pharmacies");

            migrationBuilder.DropColumn(
                name: "OwnerName",
                table: "Pharmacies");

            migrationBuilder.RenameColumn(
                name: "EmployeeName",
                table: "Demands",
                newName: "PharmacyName");

            migrationBuilder.RenameIndex(
                name: "IX_Demands_EmployeeName",
                table: "Demands",
                newName: "IX_Demands_PharmacyName");

            migrationBuilder.AddColumn<string>(
                name: "PharmacyName",
                table: "Owners",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PharmacyName",
                table: "Administrators",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Owners_PharmacyName",
                table: "Owners",
                column: "PharmacyName");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Demands_Pharmacies_PharmacyName",
                table: "Demands",
                column: "PharmacyName",
                principalTable: "Pharmacies",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Owners_Pharmacies_PharmacyName",
                table: "Owners",
                column: "PharmacyName",
                principalTable: "Pharmacies",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Administrators_Pharmacies_PharmacyName",
                table: "Administrators");

            migrationBuilder.DropForeignKey(
                name: "FK_Demands_Pharmacies_PharmacyName",
                table: "Demands");

            migrationBuilder.DropForeignKey(
                name: "FK_Owners_Pharmacies_PharmacyName",
                table: "Owners");

            migrationBuilder.DropIndex(
                name: "IX_Owners_PharmacyName",
                table: "Owners");

            migrationBuilder.DropIndex(
                name: "IX_Administrators_PharmacyName",
                table: "Administrators");

            migrationBuilder.DropColumn(
                name: "PharmacyName",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "PharmacyName",
                table: "Administrators");

            migrationBuilder.RenameColumn(
                name: "PharmacyName",
                table: "Demands",
                newName: "EmployeeName");

            migrationBuilder.RenameIndex(
                name: "IX_Demands_PharmacyName",
                table: "Demands",
                newName: "IX_Demands_EmployeeName");

            migrationBuilder.AddColumn<string>(
                name: "AdministratorName",
                table: "Pharmacies",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerName",
                table: "Pharmacies",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacies_AdministratorName",
                table: "Pharmacies",
                column: "AdministratorName");

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacies_OwnerName",
                table: "Pharmacies",
                column: "OwnerName");

            migrationBuilder.AddForeignKey(
                name: "FK_Demands_Employees_EmployeeName",
                table: "Demands",
                column: "EmployeeName",
                principalTable: "Employees",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pharmacies_Administrators_AdministratorName",
                table: "Pharmacies",
                column: "AdministratorName",
                principalTable: "Administrators",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pharmacies_Owners_OwnerName",
                table: "Pharmacies",
                column: "OwnerName",
                principalTable: "Owners",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
