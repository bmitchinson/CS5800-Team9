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
                await context.SeedDatabaseThreeStudentsAsync();
            }
            using (var context = new ApplicationDbContext(options))
            {
                var controller = SetUpStudentController(context);

                var targetData = await 
                    context
                    .Students
                    .ToListAsync();
                
                var result = await controller.Get();
                var actionResultData = result.Value;

                Assert.Equal(targetData.Count(), actionResultData.Count());
            }
        }

        [Fact]
        public async Task GET_Returns_Student_With_Supplied_Id()
        {
            var options = SqliteInMemory
                .CreateOptions<ApplicationDbContext>();
            using (var context = new ApplicationDbContext(options))
            {
                await context.Database.EnsureCreatedAsync();
                await context.SeedDatabaseThreeStudentsAsync();
            }
            using (var context = new ApplicationDbContext(options))
            {
                var controller = SetUpStudentController(context);

                var targetData = await 
                    context
                    .Students
                    .Where(_ => _.StudentId == 1)
                    .FirstOrDefaultAsync();

                var actionResult = await controller.Get(1);
                var actionResultData = actionResult.Value;

                Assert.Equal(targetData.StudentId, actionResultData.StudentId);
                Assert.Equal(targetData.FirstName, actionResultData.FirstName);
                Assert.Equal(targetData.LastName, actionResultData.LastName);
                Assert.Equal(targetData.BirthDate, actionResultData.BirthDate);
            }
        }

        public static StudentController SetUpStudentController(ApplicationDbContext context) =>
            new StudentController(context);
    }
}