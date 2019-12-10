using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class newfieldpasswordresettoapplicationusers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResetPassword",
                table: "ApplicationUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResetPassword",
                table: "ApplicationUsers");
        }
    }
}
