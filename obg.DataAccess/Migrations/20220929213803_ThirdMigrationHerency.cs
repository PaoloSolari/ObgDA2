using Microsoft.EntityFrameworkCore.Migrations;

namespace obg.DataAccess.Migrations
{
    public partial class ThirdMigrationHerency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployeeName",
                table: "Demands",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Demands_EmployeeName",
                table: "Demands",
                column: "EmployeeName");

            migrationBuilder.AddForeignKey(
                name: "FK_Demands_Employees_EmployeeName",
                table: "Demands",
                column: "EmployeeName",
                principalTable: "Employees",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Demands_Employees_EmployeeName",
                table: "Demands");

            migrationBuilder.DropIndex(
                name: "IX_Demands_EmployeeName",
                table: "Demands");

            migrationBuilder.DropColumn(
                name: "EmployeeName",
                table: "Demands");
        }
    }
}
