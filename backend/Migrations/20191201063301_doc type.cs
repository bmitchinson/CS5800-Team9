using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class doctype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DocType",
                table: "Document",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DocType",
                table: "Document",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
