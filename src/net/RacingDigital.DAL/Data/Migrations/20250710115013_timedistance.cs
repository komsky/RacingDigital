using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RacingDigital.DAL.Migrations
{
    /// <inheritdoc />
    public partial class timedistance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DistanceBeaten",
                table: "RaceResults",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TimeBeaten",
                table: "RaceResults",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DistanceBeaten",
                table: "RaceResults");

            migrationBuilder.DropColumn(
                name: "TimeBeaten",
                table: "RaceResults");
        }
    }
}
