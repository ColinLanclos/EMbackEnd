using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backEnd_EM.Migrations
{
    /// <inheritdoc />
    public partial class forthTry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Analyses_Athletes_AthletesAthleteId",
                table: "Analyses");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Athletes_AthletesAthleteId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgressTrackers_Athletes_AthletesAthleteId",
                table: "ProgressTrackers");

            migrationBuilder.DropColumn(
                name: "AthleteId",
                table: "ProgressTrackers");

            migrationBuilder.DropColumn(
                name: "AthleteId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "AthleteId",
                table: "Analyses");

            migrationBuilder.RenameColumn(
                name: "AthletesAthleteId",
                table: "ProgressTrackers",
                newName: "AthletesId");

            migrationBuilder.RenameIndex(
                name: "IX_ProgressTrackers_AthletesAthleteId",
                table: "ProgressTrackers",
                newName: "IX_ProgressTrackers_AthletesId");

            migrationBuilder.RenameColumn(
                name: "AthleteId",
                table: "Athletes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AthletesAthleteId",
                table: "Appointments",
                newName: "AthletesId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_AthletesAthleteId",
                table: "Appointments",
                newName: "IX_Appointments_AthletesId");

            migrationBuilder.RenameColumn(
                name: "AthletesAthleteId",
                table: "Analyses",
                newName: "AthletesId");

            migrationBuilder.RenameIndex(
                name: "IX_Analyses_AthletesAthleteId",
                table: "Analyses",
                newName: "IX_Analyses_AthletesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Analyses_Athletes_AthletesId",
                table: "Analyses",
                column: "AthletesId",
                principalTable: "Athletes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Athletes_AthletesId",
                table: "Appointments",
                column: "AthletesId",
                principalTable: "Athletes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgressTrackers_Athletes_AthletesId",
                table: "ProgressTrackers",
                column: "AthletesId",
                principalTable: "Athletes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Analyses_Athletes_AthletesId",
                table: "Analyses");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Athletes_AthletesId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgressTrackers_Athletes_AthletesId",
                table: "ProgressTrackers");

            migrationBuilder.RenameColumn(
                name: "AthletesId",
                table: "ProgressTrackers",
                newName: "AthletesAthleteId");

            migrationBuilder.RenameIndex(
                name: "IX_ProgressTrackers_AthletesId",
                table: "ProgressTrackers",
                newName: "IX_ProgressTrackers_AthletesAthleteId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Athletes",
                newName: "AthleteId");

            migrationBuilder.RenameColumn(
                name: "AthletesId",
                table: "Appointments",
                newName: "AthletesAthleteId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_AthletesId",
                table: "Appointments",
                newName: "IX_Appointments_AthletesAthleteId");

            migrationBuilder.RenameColumn(
                name: "AthletesId",
                table: "Analyses",
                newName: "AthletesAthleteId");

            migrationBuilder.RenameIndex(
                name: "IX_Analyses_AthletesId",
                table: "Analyses",
                newName: "IX_Analyses_AthletesAthleteId");

            migrationBuilder.AddColumn<int>(
                name: "AthleteId",
                table: "ProgressTrackers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AthleteId",
                table: "Appointments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AthleteId",
                table: "Analyses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Analyses_Athletes_AthletesAthleteId",
                table: "Analyses",
                column: "AthletesAthleteId",
                principalTable: "Athletes",
                principalColumn: "AthleteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Athletes_AthletesAthleteId",
                table: "Appointments",
                column: "AthletesAthleteId",
                principalTable: "Athletes",
                principalColumn: "AthleteId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgressTrackers_Athletes_AthletesAthleteId",
                table: "ProgressTrackers",
                column: "AthletesAthleteId",
                principalTable: "Athletes",
                principalColumn: "AthleteId");
        }
    }
}
