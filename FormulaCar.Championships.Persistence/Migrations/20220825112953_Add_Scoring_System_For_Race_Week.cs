using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormulaCar.Championships.Persistence.Migrations
{
    public partial class Add_Scoring_System_For_Race_Week : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScoringSystems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RaceweekId = table.Column<int>(type: "int", nullable: false),
                    RegulationRuleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoringSystems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScoringSystems_Raceweeks_RaceweekId",
                        column: x => x.RaceweekId,
                        principalTable: "Raceweeks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ScoringSystems_RegulationRules_RegulationRuleId",
                        column: x => x.RegulationRuleId,
                        principalTable: "RegulationRules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScoringSystems_RaceweekId",
                table: "ScoringSystems",
                column: "RaceweekId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoringSystems_RegulationRuleId",
                table: "ScoringSystems",
                column: "RegulationRuleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScoringSystems");
        }
    }
}
