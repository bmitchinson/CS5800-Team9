using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class addeddocumentsandsubmissionstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_Registrations_RegistrationId",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Submission_Document_DocumentId",
                table: "Submission");

            migrationBuilder.DropForeignKey(
                name: "FK_Submission_StudentEnrollment_StudentEnrollmentId",
                table: "Submission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Submission",
                table: "Submission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Document",
                table: "Document");

            migrationBuilder.RenameTable(
                name: "Submission",
                newName: "Submissions");

            migrationBuilder.RenameTable(
                name: "Document",
                newName: "Documents");

            migrationBuilder.RenameIndex(
                name: "IX_Submission_StudentEnrollmentId",
                table: "Submissions",
                newName: "IX_Submissions_StudentEnrollmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Submission_DocumentId",
                table: "Submissions",
                newName: "IX_Submissions_DocumentId");

            migrationBuilder.RenameIndex(
                name: "IX_Document_RegistrationId",
                table: "Documents",
                newName: "IX_Documents_RegistrationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Submissions",
                table: "Submissions",
                column: "SubmissionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Documents",
                table: "Documents",
                column: "DocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Registrations_RegistrationId",
                table: "Documents",
                column: "RegistrationId",
                principalTable: "Registrations",
                principalColumn: "RegistrationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Documents_DocumentId",
                table: "Submissions",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "DocumentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_StudentEnrollment_StudentEnrollmentId",
                table: "Submissions",
                column: "StudentEnrollmentId",
                principalTable: "StudentEnrollment",
                principalColumn: "StudentEnrollmentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Registrations_RegistrationId",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Documents_DocumentId",
                table: "Submissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_StudentEnrollment_StudentEnrollmentId",
                table: "Submissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Submissions",
                table: "Submissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Documents",
                table: "Documents");

            migrationBuilder.RenameTable(
                name: "Submissions",
                newName: "Submission");

            migrationBuilder.RenameTable(
                name: "Documents",
                newName: "Document");

            migrationBuilder.RenameIndex(
                name: "IX_Submissions_StudentEnrollmentId",
                table: "Submission",
                newName: "IX_Submission_StudentEnrollmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Submissions_DocumentId",
                table: "Submission",
                newName: "IX_Submission_DocumentId");

            migrationBuilder.RenameIndex(
                name: "IX_Documents_RegistrationId",
                table: "Document",
                newName: "IX_Document_RegistrationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Submission",
                table: "Submission",
                column: "SubmissionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Document",
                table: "Document",
                column: "DocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_Registrations_RegistrationId",
                table: "Document",
                column: "RegistrationId",
                principalTable: "Registrations",
                principalColumn: "RegistrationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Submission_Document_DocumentId",
                table: "Submission",
                column: "DocumentId",
                principalTable: "Document",
                principalColumn: "DocumentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Submission_StudentEnrollment_StudentEnrollmentId",
                table: "Submission",
                column: "StudentEnrollmentId",
                principalTable: "StudentEnrollment",
                principalColumn: "StudentEnrollmentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
