using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormulaCar.Championships.Persistence.Migrations
{
    public partial class TableSeasonsAndRaceweekAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SeasonId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Seasons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(type: "int", nullable: false),
                    RaceNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seasons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Raceweeks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNumber = table.Column<int>(type: "int", nullable: false),
                    IsContainsSprintQualification = table.Column<bool>(type: "bit", nullable: false),
                    CircuiteId = table.Column<int>(type: "int", nullable: false),
                    SeasonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Raceweeks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Raceweeks_Circuites_CircuiteId",
                        column: x => x.CircuiteId,
                        principalTable: "Circuites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Raceweeks_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Seasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_SeasonId",
                table: "Bookings",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Raceweeks_CircuiteId",
                table: "Raceweeks",
                column: "CircuiteId");

            migrationBuilder.CreateIndex(
                name: "IX_Raceweeks_SeasonId",
                table: "Raceweeks",
                column: "SeasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Seasons_SeasonId",
                table: "Bookings",
                column: "SeasonId",
                principalTable: "Seasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Seasons_SeasonId",
                table: "Bookings");

            migrationBuilder.DropTable(
                name: "Raceweeks");

            migrationBuilder.DropTable(
                name: "Seasons");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_SeasonId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "SeasonId",
                table: "Bookings");
        }
    }
}
