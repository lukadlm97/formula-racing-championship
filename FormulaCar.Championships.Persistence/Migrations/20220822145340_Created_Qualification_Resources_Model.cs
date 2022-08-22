using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormulaCar.Championships.Persistence.Migrations
{
    public partial class Created_Qualification_Resources_Model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QualificationBestSectorTimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    SectorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualificationBestSectorTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QualificationBestSectorTimes_Result_Id",
                        column: x => x.Id,
                        principalTable: "Result",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QualificationBestSectorTimes_Sectors_SectorId",
                        column: x => x.SectorId,
                        principalTable: "Sectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "QualificationClassifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    Laps = table.Column<int>(type: "int", nullable: false),
                    QualificationPeriodId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualificationClassifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QualificationClassifications_QualificationPeriods_QualificationPeriodId",
                        column: x => x.QualificationPeriodId,
                        principalTable: "QualificationPeriods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_QualificationClassifications_Result_Id",
                        column: x => x.Id,
                        principalTable: "Result",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "QualificationMaximumSpeeds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    MaxAvgSpeed = table.Column<double>(type: "float", nullable: false),
                    SectorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualificationMaximumSpeeds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QualificationMaximumSpeeds_Result_Id",
                        column: x => x.Id,
                        principalTable: "Result",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QualificationMaximumSpeeds_Sectors_SectorId",
                        column: x => x.SectorId,
                        principalTable: "Sectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "QualificationSpeedTraps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    MaxSpeed = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualificationSpeedTraps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QualificationSpeedTraps_Result_Id",
                        column: x => x.Id,
                        principalTable: "Result",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_QualificationBestSectorTimes_SectorId",
                table: "QualificationBestSectorTimes",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_QualificationClassifications_QualificationPeriodId",
                table: "QualificationClassifications",
                column: "QualificationPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_QualificationMaximumSpeeds_SectorId",
                table: "QualificationMaximumSpeeds",
                column: "SectorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QualificationBestSectorTimes");

            migrationBuilder.DropTable(
                name: "QualificationClassifications");

            migrationBuilder.DropTable(
                name: "QualificationMaximumSpeeds");

            migrationBuilder.DropTable(
                name: "QualificationSpeedTraps");
        }
    }
}
