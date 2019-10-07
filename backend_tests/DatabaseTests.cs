using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using backend.Controllers;
using Xunit;
using TestSupport.EfHelpers;
using backend.Data.Contexts;
using Microsoft.AspNetCore.Mvc;
using backend.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace backend_tests.DatabaseTests
{
    public class DatabaseTests
    {
        [Fact]
        public async Task Seed_Database_Seeds_Data()
        {
            var options = SqliteInMemory
                .CreateOptions<ApplicationDbContext>();
            using (var context = new ApplicationDbContext(options))
            {
                await context.Database.EnsureCreatedAsync();
            }
            using (var context = new ApplicationDbContext(options))
            {

                // first ensure that the database contians no student information
                // before we begin seeding
                var students = await 
                    context
                    .Students
                    .ToListAsync();

                Assert.Empty(students);
            }
            using (var context = new ApplicationDbContext(options))
            {
                // now we seed the student data and ensure that the seeding
                // successfully added the data we wanted
                await context.TestingSeedDatabaseThreeStudentsAsync();

                var students = await
                    context
                    .Students
                    .ToListAsync();

                Assert.Equal(3, students.Count());
            }
        }
    }
}