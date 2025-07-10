using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RacingDigital.DAL.Migrations
{
    /// <inheritdoc />
    public partial class horseownerfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Horses_AspNetUsers_IdentityUserId",
                table: "Horses");

            migrationBuilder.DropIndex(
                name: "IX_Horses_IdentityUserId",
                table: "Horses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Horses_IdentityUserId",
                table: "Horses",
                column: "IdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Horses_AspNetUsers_IdentityUserId",
                table: "Horses",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
