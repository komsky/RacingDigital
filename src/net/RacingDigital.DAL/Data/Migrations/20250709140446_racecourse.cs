using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RacingDigital.DAL.Migrations
{
    /// <inheritdoc />
    public partial class racecourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Horses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Colour = table.Column<string>(type: "TEXT", nullable: true),
                    IdentityUserId = table.Column<int>(type: "INTEGER", nullable: false),
                    OwnerId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Horses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Horses_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Jockeys",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Country = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jockeys", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Racecourses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Racecourses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RaceResults",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RaceDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    RaceName = table.Column<string>(type: "TEXT", nullable: false),
                    RacecourseID = table.Column<int>(type: "INTEGER", nullable: false),
                    HorseID = table.Column<int>(type: "INTEGER", nullable: false),
                    JockeyID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceResults", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RaceResults_Horses_HorseID",
                        column: x => x.HorseID,
                        principalTable: "Horses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RaceResults_Jockeys_JockeyID",
                        column: x => x.JockeyID,
                        principalTable: "Jockeys",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RaceResults_Racecourses_RacecourseID",
                        column: x => x.RacecourseID,
                        principalTable: "Racecourses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    RaceResultId = table.Column<int>(type: "INTEGER", nullable: false),
                    IdentityUserId = table.Column<int>(type: "INTEGER", nullable: false),
                    IdentityUserId1 = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Notes_AspNetUsers_IdentityUserId1",
                        column: x => x.IdentityUserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notes_RaceResults_RaceResultId",
                        column: x => x.RaceResultId,
                        principalTable: "RaceResults",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Horses_OwnerId",
                table: "Horses",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_IdentityUserId1",
                table: "Notes",
                column: "IdentityUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_RaceResultId",
                table: "Notes",
                column: "RaceResultId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceResults_HorseID",
                table: "RaceResults",
                column: "HorseID");

            migrationBuilder.CreateIndex(
                name: "IX_RaceResults_JockeyID",
                table: "RaceResults",
                column: "JockeyID");

            migrationBuilder.CreateIndex(
                name: "IX_RaceResults_RacecourseID",
                table: "RaceResults",
                column: "RacecourseID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "RaceResults");

            migrationBuilder.DropTable(
                name: "Horses");

            migrationBuilder.DropTable(
                name: "Jockeys");

            migrationBuilder.DropTable(
                name: "Racecourses");
        }
    }
}
