using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proje.Data.Migrations
{
    /// <inheritdoc />
    public partial class uyetablechange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "kilo",
                table: "AspNetUsers",
                newName: "weight");

            migrationBuilder.RenameColumn(
                name: "boy",
                table: "AspNetUsers",
                newName: "length");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "weight",
                table: "AspNetUsers",
                newName: "kilo");

            migrationBuilder.RenameColumn(
                name: "length",
                table: "AspNetUsers",
                newName: "boy");
        }
    }
}
