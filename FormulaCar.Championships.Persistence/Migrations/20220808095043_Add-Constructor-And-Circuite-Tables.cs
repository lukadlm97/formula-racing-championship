using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormulaCar.Championships.Persistence.Migrations
{
    public partial class AddConstructorAndCircuiteTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    CountryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Circuites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Circuites_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id");
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
                    CountryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Constructors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Constructors_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Constructors_MediaTags_MediaTagId",
                        column: x => x.MediaTagId,
                        principalTable: "MediaTags",
                        principalColumn: "Id");
                });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Circuites");

            migrationBuilder.DropTable(
                name: "Constructors");
        }
    }
}
