using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backEnd_EM.Migrations
{
    /// <inheritdoc />
    public partial class Analytics2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Analytics_AthletesId",
                table: "Analytics",
                column: "AthletesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Analytics_Athletes_AthletesId",
                table: "Analytics",
                column: "AthletesId",
                principalTable: "Athletes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Analytics_Athletes_AthletesId",
                table: "Analytics");

            migrationBuilder.DropIndex(
                name: "IX_Analytics_AthletesId",
                table: "Analytics");
        }
    }
}
