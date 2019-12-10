using backend.Data.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using backend.Data.Contexts;

namespace backend.Data.QueryObjects
{
    public static class GetCompletedCourses
    {
        public static IQueryable<StudentEnrollment> 
            QueryCompletedCourses(this IQueryable<StudentEnrollment> enrollments) =>
                enrollments
                .Where(_ => _.IsCompleted)
                .Include(_ => _.Registration)
                    .ThenInclude(_ => _.Course);
    }
}