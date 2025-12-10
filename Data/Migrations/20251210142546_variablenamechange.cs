using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proje.Data.Migrations
{
    /// <inheritdoc />
    public partial class variablenamechange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Uzmanlik",
                table: "Egitmenler",
                newName: "speciality");

            migrationBuilder.RenameColumn(
                name: "DeneyimYili",
                table: "Egitmenler",
                newName: "experience");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "speciality",
                table: "Egitmenler",
                newName: "Uzmanlik");

            migrationBuilder.RenameColumn(
                name: "experience",
                table: "Egitmenler",
                newName: "DeneyimYili");
        }
    }
}
