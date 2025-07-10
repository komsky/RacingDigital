using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RacingDigital.DAL.Migrations
{
    /// <inheritdoc />
    public partial class finishingpositionfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FinishingPosition",
                table: "RaceResults",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinishingPosition",
                table: "RaceResults");
        }
    }
}
