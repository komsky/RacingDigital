using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RacingDigital.DAL.Migrations
{
    /// <inheritdoc />
    public partial class horseownerforeignkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Horses_AspNetUsers_OwnerId",
                table: "Horses");

            migrationBuilder.DropIndex(
                name: "IX_Horses_OwnerId",
                table: "Horses");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Horses");

            migrationBuilder.AlterColumn<string>(
                name: "IdentityUserId",
                table: "Horses",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Horses",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Horses_AspNetUsers_IdentityUserId",
                table: "Horses");

            migrationBuilder.DropIndex(
                name: "IX_Horses_IdentityUserId",
                table: "Horses");

            migrationBuilder.AlterColumn<int>(
                name: "IdentityUserId",
                table: "Horses",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Horses",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Horses",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Horses_OwnerId",
                table: "Horses",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Horses_AspNetUsers_OwnerId",
                table: "Horses",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
