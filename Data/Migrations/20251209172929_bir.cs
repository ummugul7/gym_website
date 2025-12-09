using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proje.Data.Migrations
{
    /// <inheritdoc />
    public partial class bir : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "boy",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "kilo",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "boy",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "kilo",
                table: "AspNetUsers");
        }
    }
}
