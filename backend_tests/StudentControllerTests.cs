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

namespace backend_tests.ControllerTests
{
    public class StudentControllerTests
    {
        [Fact]
        public async Task GET_Returns_All_Students()
        {
            var options = SqliteInMemory
                .CreateOptions<ApplicationDbContext>();
            using (var context = new ApplicationDbContext(options))
            {
                await context.Database.EnsureCreatedAsync();
                await context.SeedDatabaseThreeStudents();
            }
            using (var context = new ApplicationDbContext(options))
            {
                var controller = SetUpStudentController(context);
                var result = await controller.Get();
                var data = result.Value;

                Assert.Equal(3, data.Count());
            }
        }

        public static StudentController SetUpStudentController(ApplicationDbContext context) =>
            new StudentController(context);
    }
}