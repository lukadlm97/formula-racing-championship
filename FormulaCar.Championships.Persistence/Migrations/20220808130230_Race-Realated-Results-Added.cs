using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormulaCar.Championships.Persistence.Migrations
{
    public partial class RaceRealatedResultsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RaceFastesLaps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    LapTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Lap = table.Column<int>(type: "int", nullable: false),
                    Gap = table.Column<TimeSpan>(type: "time", nullable: false),
                    AvgSpeed = table.Column<double>(type: "float", nullable: false),
                    RegistrationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceFastesLaps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RaceFastesLaps_Result_Id",
                        column: x => x.Id,
                        principalTable: "Result",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RaceMaximumSpeeds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    MaxAvgSpeed = table.Column<int>(type: "int", nullable: false),
                    SectorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceMaximumSpeeds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RaceMaximumSpeeds_Result_Id",
                        column: x => x.Id,
                        principalTable: "Result",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RaceMaximumSpeeds_Sectors_SectorId",
                        column: x => x.SectorId,
                        principalTable: "Sectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "RacePitStops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    TotalTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RacePitStops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RacePitStops_Result_Id",
                        column: x => x.Id,
                        principalTable: "Result",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RaceSectorTimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    SectorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceSectorTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RaceSectorTimes_Result_Id",
                        column: x => x.Id,
                        principalTable: "Result",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RaceSectorTimes_Sectors_SectorId",
                        column: x => x.SectorId,
                        principalTable: "Sectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "RaceSpeedTraps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    MaxSpeed = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceSpeedTraps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RaceSpeedTraps_Result_Id",
                        column: x => x.Id,
                        principalTable: "Result",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RaceMaximumSpeeds_SectorId",
                table: "RaceMaximumSpeeds",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceSectorTimes_SectorId",
                table: "RaceSectorTimes",
                column: "SectorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RaceFastesLaps");

            migrationBuilder.DropTable(
                name: "RaceMaximumSpeeds");

            migrationBuilder.DropTable(
                name: "RacePitStops");

            migrationBuilder.DropTable(
                name: "RaceSectorTimes");

            migrationBuilder.DropTable(
                name: "RaceSpeedTraps");
        }
    }
}
