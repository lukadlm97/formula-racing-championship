using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormulaCar.Championships.Persistence.Migrations
{
    public partial class Add_More_Details_About_Circute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DrsZone",
                table: "Circuites",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Turns",
                table: "Circuites",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DrsZone",
                table: "Circuites");

            migrationBuilder.DropColumn(
                name: "Turns",
                table: "Circuites");
        }
    }
}
