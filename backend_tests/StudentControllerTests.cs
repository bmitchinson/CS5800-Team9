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
using System;
using Moq;
using Microsoft.AspNetCore.Http;

namespace backend_tests.ControllerTests
{
    public class StudentControllerTests
    {

        private Mock<HttpContext> moqContext;
        private Mock<HttpRequest> moqRequest;

        public void SetupTests()
        {
            moqContext = new Mock<HttpContext>();
            moqRequest = new Mock<HttpRequest>();
            moqContext.Setup(x => x.Request).Returns(moqRequest.Object);
        }

        [Fact]
        public async Task GET_Returns_All_Students()
        {
            var options = SqliteInMemory
                .CreateOptions<ApplicationDbContext>();
            using (var context = new ApplicationDbContext(options))
            {
                await context.Database.EnsureCreatedAsync();
                await context.TestingSeedDatabaseThreeStudentsAsync();
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

        // [Fact]
        // public async Task GET_Returns_Student_With_Supplied_Id()
        // {
        //     SetupTests();
        //     var options = SqliteInMemory
        //         .CreateOptions<ApplicationDbContext>();
        //     using (var context = new ApplicationDbContext(options))
        //     {
        //         await context.Database.EnsureCreatedAsync();
        //         await context.TestingSeedDatabaseThreeStudentsAsync();
        //     }
        //     using (var context = new ApplicationDbContext(options))
        //     {
        //         var controller = SetUpStudentController(context);
        //         var controllerCtx = new ControllerContext(moqContext);

        //         var targetData = await 
        //             context
        //             .Students
        //             .Where(_ => _.StudentId == 1)
        //             .FirstOrDefaultAsync();

        //         var actionResult = await controller.Get(1);
        //         var actionResultData = actionResult.Value;

        //         Assert.Equal(targetData.StudentId, actionResultData.StudentId);
        //         Assert.Equal(targetData.FirstName, actionResultData.FirstName);
        //         Assert.Equal(targetData.LastName, actionResultData.LastName);
        //         Assert.Equal(targetData.BirthDate, actionResultData.BirthDate);
        //     }
        // }

        [Fact]
        public async Task POST_Creates_New_Student()
        {
            var options = SqliteInMemory
                .CreateOptions<ApplicationDbContext>();
            using (var context = new ApplicationDbContext(options))
            {
                await context.Database.EnsureCreatedAsync();
            }
            using (var context = new ApplicationDbContext(options))
            {
                var controller = SetUpStudentController(context);

                var newStudent = new Student
                {
                    FirstName = "John",
                    LastName = "Doe",
                    BirthDate = DateTime.Now
                };

                await controller.Post(newStudent);
            }
            using (var context = new ApplicationDbContext(options))
            {
                var target = await
                    context
                    .Students
                    .Where(_ => _.StudentId == 1)
                    .FirstOrDefaultAsync();

                Assert.Equal("John", target.FirstName);
                Assert.Equal("Doe", target.LastName);
            }
        }

        [Fact]
        public async Task DELETE_Deletes_Student()
        {
            var options = SqliteInMemory
                .CreateOptions<ApplicationDbContext>();
            using (var context = new ApplicationDbContext(options))
            {
                await context.Database.EnsureCreatedAsync();
                await context.TestingSeedDatabaseThreeStudentsAsync();
            }
            using (var context = new ApplicationDbContext(options))
            {
                var controller = SetUpStudentController(context);

                var targetForDeletion = await 
                    context
                    .Students
                    .Where(_ => _.StudentId == 1)
                    .FirstOrDefaultAsync();

                await controller.Delete(targetForDeletion.StudentId);
            }
            using (var context = new ApplicationDbContext(options))
            {
                Assert.Equal(2, 
                    context
                    .Students
                    .Count());

                Assert.Empty(
                    context
                    .Students
                    .Where(_ => _.StudentId == 1)
                    .ToList());
            }
        }

        public static StudentController SetUpStudentController(ApplicationDbContext context) =>
            new StudentController(context);
    }
}