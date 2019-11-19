using System.Linq;
using backend.Data.Models;
using System.Collections.Generic;

namespace backend.Data.QueryObjects
{
    public static class EnrollmentsQuery
    {
        public static IQueryable<IEnumerable<StudentEnrollment>> GetStudentEnrollmentsFromStudent(
            this IQueryable<Student> students) =>
            students
            .Select(st => st.Enrollments
                .Select(enr => new StudentEnrollment
                {
                    StudentEnrollmentId = enr.StudentEnrollmentId,
                    StudentId = enr.StudentId,
                    RegistrationId = enr.RegistrationId,
                    Registration = new Registration
                    {
                        RegistrationId = enr.Registration.RegistrationId,
                        CourseId = enr.Registration.CourseId,
                        InstructorId = enr.Registration.InstructorId,
                        EnrollmentLimit = enr.Registration.EnrollmentLimit,
                        Course = new Course
                        {
                            CourseId = enr.Registration.Course.CourseId,
                            CourseName = enr.Registration.Course.CourseName,
                            CreditHours = enr.Registration.Course.CreditHours,
                            Section = enr.Registration.Course.Section,
                            StartTime = enr.Registration.Course.StartTime,
                            EndTime = enr.Registration.Course.EndTime,
                            Level = enr.Registration.Course.Level
                        },
                        Instructor = new Instructor
                        {
                            InstructorId = enr.Registration.Instructor.InstructorId,
                            FirstName = enr.Registration.Instructor.FirstName,
                            LastName = enr.Registration.Instructor.LastName,
                            Email = enr.Registration.Instructor.Email
                        },
                        Prerequisites = enr.Registration.Prerequisites
                            .Select(pre => new Prerequisite
                            {
                                CourseId = pre.CourseId,
                                PrerequisiteId = pre.PrerequisiteId,
                                IsMandatory = pre.IsMandatory,
                                Course = new Course
                                {
                                    CourseId = pre.Course.CourseId,
                                    CourseName = pre.Course.CourseName,
                                    CreditHours = pre.Course.CreditHours,
                                    Section = pre.Course.Section,
                                    StartTime = pre.Course.StartTime,
                                    EndTime = pre.Course.EndTime,
                                    Level = pre.Course.Level
                                }
                            }).ToList()
                    }
                }));

        // public static IQueryable<IEnumerable<StudentEnrollment>> GetStudentEnrollmentsFromInstructor(
        //     this IQueryable<Instructor> instructors) =>
        //     instructors
        //     .Select(_ => _.Registrations)

    }
}