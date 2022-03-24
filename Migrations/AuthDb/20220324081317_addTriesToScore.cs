using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wordle.Migrations.AuthDb
{
    public partial class addTriesToScore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Tries",
                table: "Scores",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tries",
                table: "Scores");
        }
    }
}
