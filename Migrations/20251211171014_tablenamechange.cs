using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proje.Migrations
{
    /// <inheritdoc />
    public partial class tablenamechange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Egitmenler_AspNetUsers_UserId",
                table: "Egitmenler");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Egitmenler",
                table: "Egitmenler");

            migrationBuilder.RenameTable(
                name: "Egitmenler",
                newName: "Coach");

            migrationBuilder.RenameIndex(
                name: "IX_Egitmenler_UserId",
                table: "Coach",
                newName: "IX_Coach_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Coach",
                table: "Coach",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Coach_AspNetUsers_UserId",
                table: "Coach",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coach_AspNetUsers_UserId",
                table: "Coach");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Coach",
                table: "Coach");

            migrationBuilder.RenameTable(
                name: "Coach",
                newName: "Egitmenler");

            migrationBuilder.RenameIndex(
                name: "IX_Coach_UserId",
                table: "Egitmenler",
                newName: "IX_Egitmenler_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Egitmenler",
                table: "Egitmenler",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Egitmenler_AspNetUsers_UserId",
                table: "Egitmenler",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
