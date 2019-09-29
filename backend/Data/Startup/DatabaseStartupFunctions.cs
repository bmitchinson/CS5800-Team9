using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using backend.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using backend.Data.Models;
using System.Threading.Tasks;

namespace backend.Data.Startup
{
    public static class DatabaseStartupFunctions
    {
        public static IWebHost MigrateDatabase(this IWebHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                using (var context = services.GetRequiredService<ApplicationDbContext>())
                {
                    try
                    {
                        context.Database.Migrate();
                    }

                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            return webHost;
        }

        public static IWebHost SeedDatabase(this IWebHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var env = services.GetRequiredService<IHostingEnvironment>();

                using (var context = services.GetRequiredService<ApplicationDbContext>())
                {
                    if (!context.Students.Any() && env.IsDevelopment())
                    {
                        var seededStudents = new List<Student>
                        {
                            new Student
                            {
                                FirstName = "Greg",
                                LastName = "Mich"
                            },
                            new Student
                            {
                                FirstName = "John",
                                LastName = "Smith"
                            },
                            new Student
                            {
                                FirstName = "Laura",
                                LastName = "Jackson"
                            }
                        };
                        context.AddRange(seededStudents);
                        context.SaveChanges();
                    }
                }
            }
            return webHost;
        }
    }
}