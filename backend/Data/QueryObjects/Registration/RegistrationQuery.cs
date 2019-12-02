using System.Linq;
using backend.Data.Models;

namespace backend.Data.QueryObjects
{
    public static class RegistrationQuery
    {
        public static IQueryable<Registration> GetRegistrations(
        this IQueryable<Registration> registrations) =>
            registrations
            .Select(reg => new Registration
            {
                RegistrationId = reg.RegistrationId,
                CourseId = reg.CourseId,
                InstructorId = reg.InstructorId,
                EnrollmentLimit = reg.EnrollmentLimit,
                Section = reg.Section,
                StartTime = reg.StartTime,
                EndTime = reg.EndTime,
                Prerequisites = reg.Prerequisites
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
                        }
                    })
                    .ToList(),
                Course = new Course
                {
                    CourseId = reg.Course.CourseId,
                    CourseName = reg.Course.CourseName,
                    CreditHours = reg.Course.CreditHours,
                    Level = reg.Course.Level
                },
                Instructor = new Instructor
                {
                    InstructorId = reg.Instructor.InstructorId,
                    FirstName = reg.Instructor.FirstName,
                    LastName = reg.Instructor.LastName,
                    Email = reg.Instructor.Email
                }
            });
    }
}