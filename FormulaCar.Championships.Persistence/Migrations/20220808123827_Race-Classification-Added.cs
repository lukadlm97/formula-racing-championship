using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormulaCar.Championships.Persistence.Migrations
{
    public partial class RaceClassificationAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PositionId",
                table: "Result",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_Result_PositionId",
                table: "Result",
                column: "PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Result_Positions_PositionId",
                table: "Result",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Result_Positions_PositionId",
                table: "Result");

            migrationBuilder.DropTable(
                name: "RaceClassifications");

            migrationBuilder.DropIndex(
                name: "IX_Result_PositionId",
                table: "Result");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "Result");
        }
    }
}
