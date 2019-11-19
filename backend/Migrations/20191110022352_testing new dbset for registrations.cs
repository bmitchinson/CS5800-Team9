using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class testingnewdbsetforregistrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registration_Courses_CourseId",
                table: "Registration");

            migrationBuilder.DropForeignKey(
                name: "FK_Registration_Instructors_InstructorId",
                table: "Registration");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentEnrollment_Registration_RegistrationId",
                table: "StudentEnrollment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Registration",
                table: "Registration");

            migrationBuilder.RenameTable(
                name: "Registration",
                newName: "Registrations");

            migrationBuilder.RenameIndex(
                name: "IX_Registration_InstructorId",
                table: "Registrations",
                newName: "IX_Registrations_InstructorId");

            migrationBuilder.RenameIndex(
                name: "IX_Registration_CourseId",
                table: "Registrations",
                newName: "IX_Registrations_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Registrations",
                table: "Registrations",
                column: "RegistrationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Courses_CourseId",
                table: "Registrations",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Instructors_InstructorId",
                table: "Registrations",
                column: "InstructorId",
                principalTable: "Instructors",
                principalColumn: "InstructorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentEnrollment_Registrations_RegistrationId",
                table: "StudentEnrollment",
                column: "RegistrationId",
                principalTable: "Registrations",
                principalColumn: "RegistrationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Courses_CourseId",
                table: "Registrations");

            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Instructors_InstructorId",
                table: "Registrations");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentEnrollment_Registrations_RegistrationId",
                table: "StudentEnrollment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Registrations",
                table: "Registrations");

            migrationBuilder.RenameTable(
                name: "Registrations",
                newName: "Registration");

            migrationBuilder.RenameIndex(
                name: "IX_Registrations_InstructorId",
                table: "Registration",
                newName: "IX_Registration_InstructorId");

            migrationBuilder.RenameIndex(
                name: "IX_Registrations_CourseId",
                table: "Registration",
                newName: "IX_Registration_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Registration",
                table: "Registration",
                column: "RegistrationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Registration_Courses_CourseId",
                table: "Registration",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Registration_Instructors_InstructorId",
                table: "Registration",
                column: "InstructorId",
                principalTable: "Instructors",
                principalColumn: "InstructorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentEnrollment_Registration_RegistrationId",
                table: "StudentEnrollment",
                column: "RegistrationId",
                principalTable: "Registration",
                principalColumn: "RegistrationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
