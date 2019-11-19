using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class fixedfkrelationwithmcquestionandassessment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MultipleChoiceQuestion_Assessment_AssessmentId",
                table: "MultipleChoiceQuestion");

            migrationBuilder.DropIndex(
                name: "IX_MultipleChoiceQuestion_AssessmentId",
                table: "MultipleChoiceQuestion");

            migrationBuilder.DropColumn(
                name: "AssessmentId",
                table: "MultipleChoiceQuestion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssessmentId",
                table: "MultipleChoiceQuestion",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MultipleChoiceQuestion_AssessmentId",
                table: "MultipleChoiceQuestion",
                column: "AssessmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_MultipleChoiceQuestion_Assessment_AssessmentId",
                table: "MultipleChoiceQuestion",
                column: "AssessmentId",
                principalTable: "Assessment",
                principalColumn: "AssessmentId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
