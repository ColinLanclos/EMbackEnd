using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backEnd_EM.Migrations
{
    /// <inheritdoc />
    public partial class loginAndReset1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Athletes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "Password",
                table: "Athletes",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "PasswordResetToken",
                table: "Athletes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Athletes",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResetTokenExperies",
                table: "Athletes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "VerfiedAt",
                table: "Athletes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "varificationToken",
                table: "Athletes",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Athletes");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Athletes");

            migrationBuilder.DropColumn(
                name: "PasswordResetToken",
                table: "Athletes");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Athletes");

            migrationBuilder.DropColumn(
                name: "ResetTokenExperies",
                table: "Athletes");

            migrationBuilder.DropColumn(
                name: "VerfiedAt",
                table: "Athletes");

            migrationBuilder.DropColumn(
                name: "varificationToken",
                table: "Athletes");
        }
    }
}
