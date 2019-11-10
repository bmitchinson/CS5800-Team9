using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class registrationnowcontainstheprereqs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RegistrationId",
                table: "Prerequisite",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prerequisite_RegistrationId",
                table: "Prerequisite",
                column: "RegistrationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prerequisite_Registrations_RegistrationId",
                table: "Prerequisite",
                column: "RegistrationId",
                principalTable: "Registrations",
                principalColumn: "RegistrationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prerequisite_Registrations_RegistrationId",
                table: "Prerequisite");

            migrationBuilder.DropIndex(
                name: "IX_Prerequisite_RegistrationId",
                table: "Prerequisite");

            migrationBuilder.DropColumn(
                name: "RegistrationId",
                table: "Prerequisite");
        }
    }
}
