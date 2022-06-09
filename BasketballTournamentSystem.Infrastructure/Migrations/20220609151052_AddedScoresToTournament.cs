using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasketballTournamentSystem.Data.Migrations
{
    public partial class AddedScoresToTournament : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamOneScore",
                table: "Tournaments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeamTwoScore",
                table: "Tournaments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeamOneScore",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "TeamTwoScore",
                table: "Tournaments");
        }
    }
}
