using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RacingDigital.DAL.Migrations
{
    /// <inheritdoc />
    public partial class notesownershipfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_AspNetUsers_IdentityUserId1",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_IdentityUserId1",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "IdentityUserId1",
                table: "Notes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdentityUserId",
                table: "Notes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId1",
                table: "Notes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_IdentityUserId1",
                table: "Notes",
                column: "IdentityUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_AspNetUsers_IdentityUserId1",
                table: "Notes",
                column: "IdentityUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
