using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proje.Migrations
{
    /// <inheritdoc />
    public partial class priceValueUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "price",
                table: "Appointment");

            migrationBuilder.AddColumn<int>(
                name: "price",
                table: "Coach",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "price",
                table: "Coach");

            migrationBuilder.AddColumn<int>(
                name: "price",
                table: "Appointment",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
