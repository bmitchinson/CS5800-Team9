using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class newdocumenttype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ResourceLink",
                table: "Document",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DocType",
                table: "Document",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocType",
                table: "Document");

            migrationBuilder.AlterColumn<string>(
                name: "ResourceLink",
                table: "Document",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
