using backend.Data.Contexts;
using backend.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend_tests
{
    public static class InMemoryDatabaseStartupHelpers
    {
        public static async Task SeedDatabaseThreeStudents(this ApplicationDbContext context)
        {
            var seededStudents = new List<Student>
            {
                new Student
                {
                    StudentId = 1,
                    FirstName = "Bob",
                    LastName = "Smith",
                    BirthDate = System.DateTime.UtcNow
                },
                new Student
                {
                    StudentId = 2,
                    FirstName = "Laura",
                    LastName = "Jackson",
                    BirthDate = System.DateTime.UtcNow
                },
                new Student
                {
                    StudentId = 3,
                    FirstName = "Joe",
                    LastName = "Schneider",
                    BirthDate = System.DateTime.UtcNow
                }
            };

            await context.AddRangeAsync(seededStudents);
            await context.SaveChangesAsync();
        }
    }
}