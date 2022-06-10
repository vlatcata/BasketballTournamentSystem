using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasketballTournamentSystem.Data.Migrations
{
    public partial class AddedTournamentName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Tournaments",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Tournaments");
        }
    }
}
