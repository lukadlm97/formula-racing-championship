using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormulaCar.Championships.Persistence.Migrations
{
    public partial class Add_Regulation_Point_Rules : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RegulationRules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegulationRules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScorePointRegulations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Point = table.Column<int>(type: "int", nullable: false),
                    RegulationRuleId = table.Column<int>(type: "int", nullable: false),
                    PositionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScorePointRegulations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScorePointRegulations_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ScorePointRegulations_RegulationRules_RegulationRuleId",
                        column: x => x.RegulationRuleId,
                        principalTable: "RegulationRules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScorePointRegulations_PositionId",
                table: "ScorePointRegulations",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_ScorePointRegulations_RegulationRuleId",
                table: "ScorePointRegulations",
                column: "RegulationRuleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScorePointRegulations");

            migrationBuilder.DropTable(
                name: "RegulationRules");
        }
    }
}
