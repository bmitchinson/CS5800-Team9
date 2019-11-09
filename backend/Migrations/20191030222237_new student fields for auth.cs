using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class newstudentfieldsforauth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Students",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Students",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Students");
        }
    }
}
