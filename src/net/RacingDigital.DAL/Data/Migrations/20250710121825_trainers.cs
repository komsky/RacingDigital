using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RacingDigital.DAL.Migrations
{
    /// <inheritdoc />
    public partial class trainers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RaceDistance",
                table: "RaceResults",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RaceTime",
                table: "RaceResults",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TrainerID",
                table: "RaceResults",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Trainers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainers", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RaceResults_TrainerID",
                table: "RaceResults",
                column: "TrainerID");

            migrationBuilder.AddForeignKey(
                name: "FK_RaceResults_Trainers_TrainerID",
                table: "RaceResults",
                column: "TrainerID",
                principalTable: "Trainers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RaceResults_Trainers_TrainerID",
                table: "RaceResults");

            migrationBuilder.DropTable(
                name: "Trainers");

            migrationBuilder.DropIndex(
                name: "IX_RaceResults_TrainerID",
                table: "RaceResults");

            migrationBuilder.DropColumn(
                name: "RaceDistance",
                table: "RaceResults");

            migrationBuilder.DropColumn(
                name: "RaceTime",
                table: "RaceResults");

            migrationBuilder.DropColumn(
                name: "TrainerID",
                table: "RaceResults");
        }
    }
}
