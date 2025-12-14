using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proje.Migrations
{
    /// <inheritdoc />
    public partial class addNavigationProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_AspNetUsers_MemberId",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Coach_CoachId",
                table: "Appointment");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_AspNetUsers_MemberId",
                table: "Appointment",
                column: "MemberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Coach_CoachId",
                table: "Appointment",
                column: "CoachId",
                principalTable: "Coach",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_AspNetUsers_MemberId",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Coach_CoachId",
                table: "Appointment");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_AspNetUsers_MemberId",
                table: "Appointment",
                column: "MemberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Coach_CoachId",
                table: "Appointment",
                column: "CoachId",
                principalTable: "Coach",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
