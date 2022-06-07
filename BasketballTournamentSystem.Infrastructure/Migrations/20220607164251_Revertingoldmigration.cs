using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasketballTournamentSystem.Data.Migrations
{
    public partial class Revertingoldmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasRoleRequest",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasRoleRequest",
                table: "AspNetUsers");
        }
    }
}
