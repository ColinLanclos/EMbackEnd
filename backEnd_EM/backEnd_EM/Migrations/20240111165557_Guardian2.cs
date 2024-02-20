using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backEnd_EM.Migrations
{
    /// <inheritdoc />
    public partial class Guardian2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TitleName",
                table: "Guardians",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Guardians",
                newName: "Number");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Number",
                table: "Guardians",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Guardians",
                newName: "TitleName");
        }
    }
}
