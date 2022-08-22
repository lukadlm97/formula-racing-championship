using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormulaCar.Championships.Persistence.Migrations
{
    public partial class More_Tweaks_On_Sector3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sectors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNumber = table.Column<int>(type: "int", nullable: false),
                    SectorName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sectors", x => x.Id);
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
                name: "RaceMaximumSpeeds");

            migrationBuilder.DropTable(
                name: "RaceSectorTimes");

            migrationBuilder.DropTable(
                name: "Sectors");
        }
    }
}
