using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectASP.Migrations
{
    public partial class AddedRound : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Ronde",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ronde",
                table: "Matches");
        }
    }
}
