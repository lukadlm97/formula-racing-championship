using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormulaCar.Championships.Persistence.Migrations
{
    public partial class Change_MAX_Speed_To_Double : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "MaxAvgSpeed",
                table: "RaceMaximumSpeeds",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MaxAvgSpeed",
                table: "RaceMaximumSpeeds",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
