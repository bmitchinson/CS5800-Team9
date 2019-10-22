using Microsoft.EntityFrameworkCore;
using System.Linq;
using backend.Data.Contexts;
using backend.Data.Models;
using System.Collections.Generic;

namespace backend.Data.QueryObjects
{
    public static class GetRegistrations
    {
        public static IQueryable<IEnumerable<Registration>> QueryForRegistrations
            (this IQueryable<Student> queryable, int? studentId, int? instructorId, int? courseId)
            {
                // we set the primary keys to -1 if they are null so that they
                // do not return any result after querying (pk should never be negative)
                int studentIdForQuery = studentId ?? -1;
                int instructorIdForQuery = instructorId ?? -1;
                int courseIdForQuery = courseId ?? -1;

                return queryable
                    .Select(_ => _.Registrations
                    .Where(
                        r => r.StudentId == studentIdForQuery
                        || r.InstructorId == instructorIdForQuery
                        || r.CourseId == courseIdForQuery));
            }
    }
}