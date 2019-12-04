using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class submissionadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MultipleChoiceQuestionChoice");

            migrationBuilder.DropTable(
                name: "ShortAnswerQuestion");

            migrationBuilder.DropTable(
                name: "MultipleChoiceQuestion");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "Assessment");

            migrationBuilder.AddColumn<string>(
                name: "Grade",
                table: "StudentEnrollment",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "StudentEnrollment",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Submission",
                columns: table => new
                {
                    SubmissionId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DocumentId = table.Column<int>(nullable: false),
                    StudentEnrollmentId = table.Column<int>(nullable: false),
                    Grade = table.Column<string>(nullable: true),
                    ResourceLink = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submission", x => x.SubmissionId);
                    table.ForeignKey(
                        name: "FK_Submission_Document_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Document",
                        principalColumn: "DocumentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Submission_StudentEnrollment_StudentEnrollmentId",
                        column: x => x.StudentEnrollmentId,
                        principalTable: "StudentEnrollment",
                        principalColumn: "StudentEnrollmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Submission_DocumentId",
                table: "Submission",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Submission_StudentEnrollmentId",
                table: "Submission",
                column: "StudentEnrollmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Submission");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "StudentEnrollment");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "StudentEnrollment");

            migrationBuilder.CreateTable(
                name: "Assessment",
                columns: table => new
                {
                    AssessmentId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RegistrationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assessment", x => x.AssessmentId);
                    table.ForeignKey(
                        name: "FK_Assessment_Registrations_RegistrationId",
                        column: x => x.RegistrationId,
                        principalTable: "Registrations",
                        principalColumn: "RegistrationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    QuestionId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AssessmentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_Question_Assessment_AssessmentId",
                        column: x => x.AssessmentId,
                        principalTable: "Assessment",
                        principalColumn: "AssessmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MultipleChoiceQuestion",
                columns: table => new
                {
                    MultipleChoiceQuestionId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Answer = table.Column<string>(nullable: true),
                    QuestionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultipleChoiceQuestion", x => x.MultipleChoiceQuestionId);
                    table.ForeignKey(
                        name: "FK_MultipleChoiceQuestion_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShortAnswerQuestion",
                columns: table => new
                {
                    ShortAnswerQuestionId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    QuestionId = table.Column<int>(nullable: false),
                    QuestionText = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShortAnswerQuestion", x => x.ShortAnswerQuestionId);
                    table.ForeignKey(
                        name: "FK_ShortAnswerQuestion_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MultipleChoiceQuestionChoice",
                columns: table => new
                {
                    MultipleChoiceQuestionChoiceId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MultipleChoiceQuestionId = table.Column<int>(nullable: false),
                    QuestionText = table.Column<string>(nullable: true),
                    SelectionIdentifier = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultipleChoiceQuestionChoice", x => x.MultipleChoiceQuestionChoiceId);
                    table.ForeignKey(
                        name: "FK_MultipleChoiceQuestionChoice_MultipleChoiceQuestion_Multiple~",
                        column: x => x.MultipleChoiceQuestionId,
                        principalTable: "MultipleChoiceQuestion",
                        principalColumn: "MultipleChoiceQuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assessment_RegistrationId",
                table: "Assessment",
                column: "RegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_MultipleChoiceQuestion_QuestionId",
                table: "MultipleChoiceQuestion",
                column: "QuestionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MultipleChoiceQuestionChoice_MultipleChoiceQuestionId",
                table: "MultipleChoiceQuestionChoice",
                column: "MultipleChoiceQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_AssessmentId",
                table: "Question",
                column: "AssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ShortAnswerQuestion_QuestionId",
                table: "ShortAnswerQuestion",
                column: "QuestionId",
                unique: true);
        }
    }
}
