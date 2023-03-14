using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScorersReporter.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScorersReport");

            migrationBuilder.AddColumn<int>(
                name: "Assists",
                table: "Scorers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Club",
                table: "Scorers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Scorers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Scorers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Scorers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Goals",
                table: "Scorers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Scorers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "League",
                table: "Scorers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MarketValue",
                table: "Scorers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Assists",
                table: "Scorers");

            migrationBuilder.DropColumn(
                name: "Club",
                table: "Scorers");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Scorers");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Scorers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Scorers");

            migrationBuilder.DropColumn(
                name: "Goals",
                table: "Scorers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Scorers");

            migrationBuilder.DropColumn(
                name: "League",
                table: "Scorers");

            migrationBuilder.DropColumn(
                name: "MarketValue",
                table: "Scorers");

            migrationBuilder.CreateTable(
                name: "ScorersReport",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Assists = table.Column<int>(type: "int", nullable: false),
                    Club = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Goals = table.Column<int>(type: "int", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    League = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MarketValue = table.Column<int>(type: "int", nullable: false),
                    SeedScorersReportId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScorersReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScorersReport_Scorers_SeedScorersReportId",
                        column: x => x.SeedScorersReportId,
                        principalTable: "Scorers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScorersReport_SeedScorersReportId",
                table: "ScorersReport",
                column: "SeedScorersReportId");
        }
    }
}
