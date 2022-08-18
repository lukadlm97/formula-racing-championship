using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormulaCar.Championships.Persistence.Migrations
{
    public partial class Init_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MediaTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WebName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rank = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QualificationPeriods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNumber = table.Column<int>(type: "int", nullable: false),
                    PeriodName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortPeriodName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualificationPeriods", x => x.Id);
                });

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
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MediaTagId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Countries_MediaTags_MediaTagId",
                        column: x => x.MediaTagId,
                        principalTable: "MediaTags",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Circuites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Length = table.Column<double>(type: "float", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    MediaTagId = table.Column<int>(type: "int", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Circuites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Circuites_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Circuites_MediaTags_MediaTagId",
                        column: x => x.MediaTagId,
                        principalTable: "MediaTags",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Constructors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    FirstApperance = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    MediaTagId = table.Column<int>(type: "int", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Constructors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Constructors_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Constructors_MediaTags_MediaTagId",
                        column: x => x.MediaTagId,
                        principalTable: "MediaTags",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    MediaTagId = table.Column<int>(type: "int", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drivers_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Drivers_MediaTags_MediaTagId",
                        column: x => x.MediaTagId,
                        principalTable: "MediaTags",
                        principalColumn: "Id");
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
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactLenght = table.Column<int>(type: "int", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DriverId = table.Column<int>(type: "int", nullable: false),
                    ConstructorId = table.Column<int>(type: "int", nullable: false),
                    SeasonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Constructors_ConstructorId",
                        column: x => x.ConstructorId,
                        principalTable: "Constructors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Bookings_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Bookings_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Seasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Result",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    RaceweekId = table.Column<int>(type: "int", nullable: false),
                    PositionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Result", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Result_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Result_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Result_Raceweeks_RaceweekId",
                        column: x => x.RaceweekId,
                        principalTable: "Raceweeks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "RaceClassifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Laps = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceClassifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RaceClassifications_Result_Id",
                        column: x => x.Id,
                        principalTable: "Result",
                        principalColumn: "Id");
                });

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
                name: "IX_Bookings_ConstructorId",
                table: "Bookings",
                column: "ConstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_DriverId",
                table: "Bookings",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_SeasonId",
                table: "Bookings",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Circuites_CountryId",
                table: "Circuites",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Circuites_MediaTagId",
                table: "Circuites",
                column: "MediaTagId");

            migrationBuilder.CreateIndex(
                name: "IX_Constructors_CountryId",
                table: "Constructors",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Constructors_MediaTagId",
                table: "Constructors",
                column: "MediaTagId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_MediaTagId",
                table: "Countries",
                column: "MediaTagId");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_CountryId",
                table: "Drivers",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_MediaTagId",
                table: "Drivers",
                column: "MediaTagId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceMaximumSpeeds_SectorId",
                table: "RaceMaximumSpeeds",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceSectorTimes_SectorId",
                table: "RaceSectorTimes",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Raceweeks_CircuiteId",
                table: "Raceweeks",
                column: "CircuiteId");

            migrationBuilder.CreateIndex(
                name: "IX_Raceweeks_SeasonId",
                table: "Raceweeks",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Result_BookingId",
                table: "Result",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Result_PositionId",
                table: "Result",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Result_RaceweekId",
                table: "Result",
                column: "RaceweekId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QualificationPeriods");

            migrationBuilder.DropTable(
                name: "RaceClassifications");

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

            migrationBuilder.DropTable(
                name: "Sectors");

            migrationBuilder.DropTable(
                name: "Result");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Raceweeks");

            migrationBuilder.DropTable(
                name: "Constructors");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "Circuites");

            migrationBuilder.DropTable(
                name: "Seasons");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "MediaTags");
        }
    }
}
