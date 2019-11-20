using System.Linq;
using backend.Data.Models;

namespace backend.Data.QueryObjects
{
    public static class StudentQuery
    {
        // This query object selects the student with
        // some related entities. You must still use other
        // LINQ commands or EF Core commands after calling
        // this function since it returns a queryable type
        public static IQueryable<Student> GetStudents(
            this IQueryable<Student> students) =>
                students
                // we use Select Loading to explicitly define
                // what we want to query for, failure to do so may
                // cause a 
                .Select(student => new Student
                {
                    StudentId = student.StudentId,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    BirthDate = student.BirthDate,
                    Enrollments = student.Enrollments
                        .Select(enrollment => new StudentEnrollment
                        {
                            StudentEnrollmentId = enrollment.StudentEnrollmentId,
                            StudentId = enrollment.StudentId,
                            RegistrationId = enrollment.RegistrationId,
                            Registration = enrollment.Registration
                        })
                        .ToList()
                });
    }
}