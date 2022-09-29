using Microsoft.EntityFrameworkCore.Migrations;

namespace obg.DataAccess.Migrations
{
    public partial class SecondMigrationKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PharmacyName",
                table: "Users",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Name");

            migrationBuilder.CreateTable(
                name: "Demands",
                columns: table => new
                {
                    IdDemand = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Demands", x => x.IdDemand);
                });

            migrationBuilder.CreateTable(
                name: "Pharmacies",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AdministratorName = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pharmacies", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Pharmacies_Users_AdministratorName",
                        column: x => x.AdministratorName,
                        principalTable: "Users",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pharmacies_Users_OwnerName",
                        column: x => x.OwnerName,
                        principalTable: "Users",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    IdPurchase = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    BuyerEmail = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.IdPurchase);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    IdSession = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.IdSession);
                });

            migrationBuilder.CreateTable(
                name: "Petitions",
                columns: table => new
                {
                    IdPetition = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MedicineCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewQuantity = table.Column<int>(type: "int", nullable: false),
                    DemandIdDemand = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Petitions", x => x.IdPetition);
                    table.ForeignKey(
                        name: "FK_Petitions_Demands_DemandIdDemand",
                        column: x => x.DemandIdDemand,
                        principalTable: "Demands",
                        principalColumn: "IdDemand",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Invitations",
                columns: table => new
                {
                    IdInvitation = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PharmacyName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserRole = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitations", x => x.IdInvitation);
                    table.ForeignKey(
                        name: "FK_Invitations_Pharmacies_PharmacyName",
                        column: x => x.PharmacyName,
                        principalTable: "Pharmacies",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Medicines",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SymtompsItTreats = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Presentation = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Prescription = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    PharmacyName = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicines", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Medicines_Pharmacies_PharmacyName",
                        column: x => x.PharmacyName,
                        principalTable: "Pharmacies",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseLines",
                columns: table => new
                {
                    IdPurchaseLine = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MedicineCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedicineQuantity = table.Column<int>(type: "int", nullable: false),
                    PurchaseIdPurchase = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseLines", x => x.IdPurchaseLine);
                    table.ForeignKey(
                        name: "FK_PurchaseLines_Purchases_PurchaseIdPurchase",
                        column: x => x.PurchaseIdPurchase,
                        principalTable: "Purchases",
                        principalColumn: "IdPurchase",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_PharmacyName",
                table: "Users",
                column: "PharmacyName");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_PharmacyName",
                table: "Invitations",
                column: "PharmacyName");

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_PharmacyName",
                table: "Medicines",
                column: "PharmacyName");

            migrationBuilder.CreateIndex(
                name: "IX_Petitions_DemandIdDemand",
                table: "Petitions",
                column: "DemandIdDemand");

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacies_AdministratorName",
                table: "Pharmacies",
                column: "AdministratorName");

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacies_OwnerName",
                table: "Pharmacies",
                column: "OwnerName");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseLines_PurchaseIdPurchase",
                table: "PurchaseLines",
                column: "PurchaseIdPurchase");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Pharmacies_PharmacyName",
                table: "Users",
                column: "PharmacyName",
                principalTable: "Pharmacies",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Pharmacies_PharmacyName",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Invitations");

            migrationBuilder.DropTable(
                name: "Medicines");

            migrationBuilder.DropTable(
                name: "Petitions");

            migrationBuilder.DropTable(
                name: "PurchaseLines");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Pharmacies");

            migrationBuilder.DropTable(
                name: "Demands");

            migrationBuilder.DropTable(
                name: "Purchases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PharmacyName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PharmacyName",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");
        }
    }
}
