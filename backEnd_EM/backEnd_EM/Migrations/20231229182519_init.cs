using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace backEnd_EM.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Athletes",
                columns: table => new
                {
                    AthleteId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Guardian = table.Column<string>(type: "text", nullable: false),
                    Birthday = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Classification = table.Column<string>(type: "text", nullable: false),
                    Height = table.Column<string>(type: "text", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    School = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Athletes", x => x.AthleteId);
                });

            migrationBuilder.CreateTable(
                name: "Analyses",
                columns: table => new
                {
                    AnalyseId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    videoURL = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    BreakDown = table.Column<string>(type: "text", nullable: false),
                    Day = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AthleteId = table.Column<int>(type: "integer", nullable: false),
                    AthletesAthleteId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analyses", x => x.AnalyseId);
                    table.ForeignKey(
                        name: "FK_Analyses_Athletes_AthletesAthleteId",
                        column: x => x.AthletesAthleteId,
                        principalTable: "Athletes",
                        principalColumn: "AthleteId");
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    AppointmentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TypeApointment = table.Column<string>(type: "text", nullable: false),
                    Time = table.Column<float>(type: "real", nullable: false),
                    Discription = table.Column<string>(type: "text", nullable: false),
                    AthleteId = table.Column<int>(type: "integer", nullable: false),
                    AthletesAthleteId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_Appointments_Athletes_AthletesAthleteId",
                        column: x => x.AthletesAthleteId,
                        principalTable: "Athletes",
                        principalColumn: "AthleteId");
                });

            migrationBuilder.CreateTable(
                name: "ProgressTrackers",
                columns: table => new
                {
                    ProgressTrackerId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    WorkOut = table.Column<string>(type: "text", nullable: false),
                    Value_Dor_Date = table.Column<float>(type: "real", nullable: false),
                    AthleteId = table.Column<int>(type: "integer", nullable: false),
                    AthletesAthleteId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgressTrackers", x => x.ProgressTrackerId);
                    table.ForeignKey(
                        name: "FK_ProgressTrackers_Athletes_AthletesAthleteId",
                        column: x => x.AthletesAthleteId,
                        principalTable: "Athletes",
                        principalColumn: "AthleteId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Analyses_AthletesAthleteId",
                table: "Analyses",
                column: "AthletesAthleteId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_AthletesAthleteId",
                table: "Appointments",
                column: "AthletesAthleteId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgressTrackers_AthletesAthleteId",
                table: "ProgressTrackers",
                column: "AthletesAthleteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Analyses");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "ProgressTrackers");

            migrationBuilder.DropTable(
                name: "Athletes");
        }
    }
}
