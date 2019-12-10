using System.Linq;
using backend.Data.Models;

namespace backend.Data.QueryObjects
{
    public static class InstructorQuery
    {
        // This query object selects the student with
        // some related entities. You must still use other
        // LINQ commands or EF Core commands after calling
        // this function since it returns a queryable type
        public static IQueryable<Instructor> GetInstructors(
            this IQueryable<Instructor> instructors) =>
                instructors
                // we use Select Loading to explicitly define
                // what we want to query for, failure to do so may
                // cause a 
                .Select(instructor => new Instructor
                {
                    InstructorId = instructor.InstructorId,
                    FirstName = instructor.FirstName,
                    LastName = instructor.LastName,
                    BirthDate = instructor.BirthDate,
                    Registrations = instructor.Registrations
                        .Select(registration => new Registration
                        {
                            RegistrationId = registration.RegistrationId,
                            CourseId = registration.CourseId,
                            InstructorId = registration.InstructorId,
                            EnrollmentLimit = registration.EnrollmentLimit,
                            Section = registration.Section,
                            StartTime = registration.StartTime,
                            EndTime = registration.EndTime,
                            Prerequisites = registration.Prerequisites
                        })
                        .ToList()
                });
    }
}