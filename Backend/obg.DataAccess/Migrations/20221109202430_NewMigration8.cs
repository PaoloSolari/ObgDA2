using Microsoft.EntityFrameworkCore.Migrations;

namespace obg.DataAccess.Migrations
{
    public partial class NewMigration8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdministratorName",
                table: "Invitations",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdministratorName",
                table: "Invitations");
        }
    }
}
