using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proje.Data.Migrations
{
    /// <inheritdoc />
    public partial class memberAddedToCoachClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Egitmenler",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Egitmenler_UserId",
                table: "Egitmenler",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Egitmenler_AspNetUsers_UserId",
                table: "Egitmenler",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Egitmenler_AspNetUsers_UserId",
                table: "Egitmenler");

            migrationBuilder.DropIndex(
                name: "IX_Egitmenler_UserId",
                table: "Egitmenler");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Egitmenler",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
