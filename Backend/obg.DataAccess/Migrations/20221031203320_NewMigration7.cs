using Microsoft.EntityFrameworkCore.Migrations;

namespace obg.DataAccess.Migrations
{
    public partial class NewMigration7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmed",
                table: "Purchases",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PharmacyName",
                table: "Purchases",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "PurchaseLines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "WasUsed",
                table: "Invitations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_PharmacyName",
                table: "Purchases",
                column: "PharmacyName");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Pharmacies_PharmacyName",
                table: "Purchases",
                column: "PharmacyName",
                principalTable: "Pharmacies",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Pharmacies_PharmacyName",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_PharmacyName",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "PharmacyName",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "PurchaseLines");

            migrationBuilder.DropColumn(
                name: "WasUsed",
                table: "Invitations");
        }
    }
}
