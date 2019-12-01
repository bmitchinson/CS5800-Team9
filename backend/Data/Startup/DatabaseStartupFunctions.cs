using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using backend.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using backend.Data.Models;
using backend.Infrastructure.PasswordSecurity;
using System.Threading.Tasks;
using backend.Data.Enums;

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
                    if (
                        // need to check that ALL entities are empty
                        // before we decide that we want to seed the db
                        !context.Students.Any()
                        && !context.Instructors.Any()
                        && !context.Courses.Any()
                        && !context.Administrators.Any()
                        && !context.Students
                            .Select(_ => _.Enrollments)
                            .Any()
                        && !context.Courses
                            .Select(_ => _.Registrations)
                            .Any()
                        && env.IsDevelopment())
                    {
                        /*
                        Note that when data is added to an EF Core Context is not neccassary
                        that you assign the primary key (id). In fact you should almost never
                        directly manipulate the id since EF Core and the database provider will
                        manage the keys for you via auto incrementation
                         */
                        var seededStudents = new List<Student>
                        {
                            new Student
                            {
                                StudentId = 1,
                                FirstName = "Greg",
                                LastName = "Gallagher",
                                BirthDate = new DateTime(1993,12,21),
                                Email = "email1@test.com",
                                Password = PasswordSecurity.HashPassword("secret")
                            },
                            new Student
                            {
                                StudentId = 2,
                                FirstName = "John",
                                LastName = "Smith",
                                BirthDate = new DateTime(1997, 7, 23),
                                Email = "email2@test.com",
                                Password = PasswordSecurity.HashPassword("secret")
                            },
                            new Student
                            {
                                StudentId = 3,
                                FirstName = "Laura",
                                LastName = "Jackson",
                                BirthDate = new DateTime(2001, 1, 13),
                                Email = "email3@test.com",
                                Password = PasswordSecurity.HashPassword("secret")
                            },
                            new Student
                            {
                                StudentId = 4,
                                FirstName = "Ben",
                                LastName = "Mitchinson",
                                BirthDate = new DateTime(2001, 1, 13),
                                Email = "email4@test.com",
                                Password = PasswordSecurity.HashPassword("secret")
                            },
                            new Student
                            {
                                StudentId = 5,
                                FirstName = "Jacob",
                                LastName = "Watters",
                                BirthDate = new DateTime(2001, 1, 13),
                                Email = "email5@test.com",
                                Password = PasswordSecurity.HashPassword("secret")
                            },
                            new Student
                            {
                                StudentId = 6,
                                FirstName = "Greg",
                                LastName = "Mich",
                                BirthDate = new DateTime(2001, 1, 13),
                                Email = "email6@test.com",
                                Password = PasswordSecurity.HashPassword("secret")
                            },
                            new Student
                            {
                                StudentId = 7,
                                FirstName = "Alex",
                                LastName = "Powers",
                                BirthDate = new DateTime(2001, 1, 13),
                                Email = "email7@test.com",
                                Password = PasswordSecurity.HashPassword("secret")
                            },
                            new Student
                            {
                                StudentId = 8,
                                FirstName = "Nick",
                                LastName = "Grove",
                                BirthDate = new DateTime(2001, 1, 13),
                                Email = "email8@test.com",
                                Password = PasswordSecurity.HashPassword("secret")
                            },
                            new Student
                            {
                                StudentId = 9,
                                FirstName = "Griffin",
                                LastName = "Fox",
                                BirthDate = new DateTime(2001, 1, 13),
                                Email = "email9@test.com",
                                Password = PasswordSecurity.HashPassword("secret")
                            },
                            new Student
                            {
                                StudentId = 10,
                                FirstName = "Matt",
                                LastName = "Stogs",
                                BirthDate = new DateTime(2001, 1, 13),
                                Email = "email10@test.com",
                                Password = PasswordSecurity.HashPassword("secret")
                            },
                            new Student
                            {
                                StudentId = 11,
                                FirstName = "Mia",
                                LastName = "Mitchinson",
                                BirthDate = new DateTime(2001, 1, 13),
                                Email = "email11@test.com",
                                Password = PasswordSecurity.HashPassword("secret")
                            },
                            new Student
                            {
                                StudentId = 12,
                                FirstName = "Quincy",
                                LastName = "Jones",
                                BirthDate = new DateTime(2001, 1, 13),
                                Email = "email12@test.com",
                                Password = PasswordSecurity.HashPassword("secret")
                            },
                            new Student
                            {
                                StudentId = 13,
                                FirstName = "Jilly",
                                LastName = "William",
                                BirthDate = new DateTime(2001, 1, 13),
                                Email = "email13@test.com",
                                Password = PasswordSecurity.HashPassword("secret")
                            },
                            new Student
                            {
                                StudentId = 14,
                                FirstName = "Meghan",
                                LastName = "Nance",
                                BirthDate = new DateTime(2001, 1, 13),
                                Email = "email14@test.com",
                                Password = PasswordSecurity.HashPassword("secret")
                            },
                            new Student
                            {
                                StudentId = 15,
                                FirstName = "Olivia",
                                LastName = "Sandvold",
                                BirthDate = new DateTime(2001, 1, 13),
                                Email = "email15@test.com",
                                Password = PasswordSecurity.HashPassword("secret")
                            }
                        };

                        var seededInstructors = new List<Instructor>
                        {
                            new Instructor
                            {
                                InstructorId = 1,
                                FirstName = "Jackson",
                                LastName = "Crawford",
                                Email = "jcrawford@test.com",
                                Password = PasswordSecurity.HashPassword("secret")
                            },
                            new Instructor
                            {
                                InstructorId = 2,
                                FirstName = "Maggie",
                                LastName = "Ellis",
                                Email = "mellis@test.com",
                                Password = PasswordSecurity.HashPassword("secret")
                            },
                            new Instructor
                            {
                                InstructorId = 3,
                                FirstName = "Alex",
                                LastName = "Smith",
                                Email = "smith@test.com",
                                Password = PasswordSecurity.HashPassword("secret")
                            },
                            new Instructor
                            {
                                InstructorId = 4,
                                FirstName = "Hans",
                                LastName = "Johnson",
                                Email = "johnson@test.com",
                                Password = PasswordSecurity.HashPassword("secret")
                            },
                            new Instructor
                            {
                                InstructorId = 5,
                                FirstName = "John",
                                LastName = "Lim",
                                Email = "lim@test.com",
                                Password = PasswordSecurity.HashPassword("secret")
                            },
                            new Instructor
                            {
                                InstructorId = 6,
                                FirstName = "Lisa",
                                LastName = "Dorothy",
                                Email = "dorothy@test.com",
                                Password = PasswordSecurity.HashPassword("secret")
                            }
                        };

                        var seededAdmins = new List<Administrator>
                        {
                            new Administrator
                            {
                                FirstName = "Marcus",
                                LastName = "Weiss",
                                Email = "admin@test.com",
                                Password = PasswordSecurity.HashPassword("admin")
                            }
                        };

                        var seededCourses = new List<Course>
                        {
                            new Course
                            {
                                CourseId = 1,
                                CourseName = "Chemistry I",
                                CreditHours = 4,
                                Section = "CHEM:1110",
                                StartTime = DateTime.Parse("9:00 AM"),
                                EndTime = DateTime.Parse("10:45 AM")
                            },
                            new Course
                            {
                                CourseId = 2,
                                CourseName = "Chemistry II",
                                CreditHours = 4,
                                Section = "CHEM:2220",
                                StartTime = DateTime.Parse("2:30 PM"),
                                EndTime = DateTime.Parse("3:20 PM")
                            },
                            new Course
                            {
                                CourseId = 3,
                                CourseName = "Fundamentals of Software Engineering",
                                CreditHours = 3,
                                Section = "CS:5800",
                                StartTime = DateTime.Parse("5:30 PM"),
                                EndTime = DateTime.Parse("7:00 PM")
                            },
                            new Course
                            {
                                CourseId = 4,
                                CourseName = "Physics I",
                                CreditHours = 4,
                                Section = "Phys:1611",
                                StartTime = DateTime.Parse("5:30 PM"),
                                EndTime = DateTime.Parse("7:00 PM")
                            },
                            new Course
                            {
                                CourseId = 5,
                                CourseName = "Math II",
                                CreditHours = 3,
                                Section = "MATH:1560",
                                StartTime = DateTime.Parse("5:30 PM"),
                                EndTime = DateTime.Parse("7:00 PM")
                            },
                            new Course
                            {
                                CourseId = 6,
                                CourseName = "Physics II",
                                CreditHours = 3,
                                Section = "MATH:1560",
                                StartTime = DateTime.Parse("5:30 PM"),
                                EndTime = DateTime.Parse("7:00 PM")
                            }
                        };

                        var seededRegistrations = new List<Registration>
                        {
                            new Registration
                            {
                                RegistrationId = 1,
                                CourseId = 1, // Chem 1
                                InstructorId = 1,
                                EnrollmentLimit = 40,
                            },
                            new Registration
                            {
                                RegistrationId = 2,
                                CourseId = 2, // Chem 2
                                InstructorId = 1,
                                EnrollmentLimit = 20,
                                Prerequisites = new List<Prerequisite>
                                {
                                    new Prerequisite
                                    {
                                        CourseId = 1
                                    }
                                }
                            },
                            new Registration
                            {
                                RegistrationId = 3,
                                CourseId = 3, // SE
                                InstructorId = 2,
                                EnrollmentLimit = 20
                            },
                            new Registration
                            {
                                RegistrationId = 4,
                                CourseId = 3, // SE
                                InstructorId = 3,
                                EnrollmentLimit = 20
                            },
                            new Registration
                            {
                                RegistrationId = 5,
                                CourseId = 4, // Phys 1
                                InstructorId = 4,
                                EnrollmentLimit = 25
                            },
                            new Registration
                            {
                                RegistrationId = 6,
                                CourseId = 5, // Math II
                                InstructorId = 5,
                                EnrollmentLimit = 40
                            },
                            new Registration
                            {
                                RegistrationId = 7,
                                CourseId = 6, // Phys II
                                InstructorId = 6,
                                EnrollmentLimit = 30,
                                Prerequisites = new List<Prerequisite>
                                {
                                    new Prerequisite
                                    {
                                        CourseId = 4
                                    }
                                }
                            }
                        };

                        var seededStudentEnrollments = new List<StudentEnrollment>
                        {
                            new StudentEnrollment
                            {
                                StudentId = 1,
                                StudentEnrollmentId = 1,
                                RegistrationId = 1
                            },
                            new StudentEnrollment
                            {
                                StudentId = 1,
                                StudentEnrollmentId = 2,
                                RegistrationId = 3
                            },
                            new StudentEnrollment
                            {
                                StudentId = 1,
                                StudentEnrollmentId = 3,
                                RegistrationId = 5
                            },
                            new StudentEnrollment
                            {
                                StudentId = 2,
                                StudentEnrollmentId = 4,
                                RegistrationId = 1
                            },
                            new StudentEnrollment
                            {
                                StudentId = 2,
                                StudentEnrollmentId = 5,
                                RegistrationId = 5
                            },
                            new StudentEnrollment
                            {
                                StudentId = 3,
                                StudentEnrollmentId = 6,
                                RegistrationId = 6
                            },
                            new StudentEnrollment
                            {
                                StudentId = 4,
                                StudentEnrollmentId = 7,
                                RegistrationId = 3
                            },
                            new StudentEnrollment
                            {
                                StudentId = 4,
                                StudentEnrollmentId = 8,
                                RegistrationId = 4
                            },
                            new StudentEnrollment
                            {
                                StudentId = 4,
                                StudentEnrollmentId = 9,
                                RegistrationId = 5
                            },
                            new StudentEnrollment
                            {
                                StudentId = 5,
                                StudentEnrollmentId = 10,
                                RegistrationId = 4
                            },
                            new StudentEnrollment
                            {
                                StudentId = 5,
                                StudentEnrollmentId = 11,
                                RegistrationId = 6
                            },
                            new StudentEnrollment
                            {
                                StudentId = 6,
                                StudentEnrollmentId = 12,
                                RegistrationId = 6
                            },
                            new StudentEnrollment
                            {
                                StudentId = 6,
                                StudentEnrollmentId = 13,
                                RegistrationId = 4
                            },
                            new StudentEnrollment
                            {
                                StudentId = 7,
                                StudentEnrollmentId = 14,
                                RegistrationId = 1
                            },
                            new StudentEnrollment
                            {
                                StudentId = 7,
                                StudentEnrollmentId = 15,
                                RegistrationId = 3
                            },
                            new StudentEnrollment
                            {
                                StudentId = 8,
                                StudentEnrollmentId = 16,
                                RegistrationId = 2
                            },
                            new StudentEnrollment
                            {
                                StudentId = 8,
                                StudentEnrollmentId = 17,
                                RegistrationId = 6
                            },
                            new StudentEnrollment
                            {
                                StudentId = 9,
                                StudentEnrollmentId = 18,
                                RegistrationId = 2
                            },
                            new StudentEnrollment
                            {
                                StudentId = 9,
                                StudentEnrollmentId = 19,
                                RegistrationId = 6
                            },
                            new StudentEnrollment
                            {
                                StudentId = 10,
                                StudentEnrollmentId = 20,
                                RegistrationId = 6
                            },
                            new StudentEnrollment
                            {
                                StudentId = 10,
                                StudentEnrollmentId = 21,
                                RegistrationId = 4
                            },
                            new StudentEnrollment
                            {
                                StudentId = 11,
                                StudentEnrollmentId = 22,
                                RegistrationId = 4
                            },
                            new StudentEnrollment
                            {
                                StudentId = 11,
                                StudentEnrollmentId = 23,
                                RegistrationId = 2
                            },
                            new StudentEnrollment
                            {
                                StudentId = 12,
                                StudentEnrollmentId = 24,
                                RegistrationId = 2
                            },
                            new StudentEnrollment
                            {
                                StudentId = 12,
                                StudentEnrollmentId = 25,
                                RegistrationId = 3
                            },
                            new StudentEnrollment
                            {
                                StudentId = 13,
                                StudentEnrollmentId = 26,
                                RegistrationId = 1
                            },
                            new StudentEnrollment
                            {
                                StudentId = 13,
                                StudentEnrollmentId = 27,
                                RegistrationId = 5
                            },
                            new StudentEnrollment
                            {
                                StudentId = 14,
                                StudentEnrollmentId = 28,
                                RegistrationId = 5
                            },
                            new StudentEnrollment
                            {
                                StudentId = 14,
                                StudentEnrollmentId = 29,
                                RegistrationId = 2
                            },
                            new StudentEnrollment
                            {
                                StudentId = 15,
                                StudentEnrollmentId = 30,
                                RegistrationId = 4
                            }
                        };

                        var seededDocuments = new List<Document>
                        {
                            new Document
                            {
                                RegistrationId = 1,
                                ResourceLink = "https://quiz1.pdf@test.com",
                                DocType = DocumentType.Quiz
                            },
                            new Document
                            {
                                RegistrationId = 1,
                                ResourceLink = "https://homework1.pdf@test.com",
                                DocType = DocumentType.Assignment
                            },
                            new Document
                            {
                                RegistrationId = 1,
                                ResourceLink = "https://quiz2.pdf@test.com",
                                DocType = DocumentType.Quiz
                            },
                            new Document
                            {
                                RegistrationId = 2,
                                ResourceLink = "https://quiz1.pdf@test.com",
                                DocType = DocumentType.Quiz
                            },
                            new Document
                            {
                                RegistrationId = 2,
                                ResourceLink = "https://homework.pdf@test.com",
                                DocType = DocumentType.Exam
                            }
                        };

                        context.AddRange(seededStudents);
                        context.AddRange(seededCourses);
                        context.AddRange(seededInstructors);
                        context.AddRange(seededAdmins);
                        context.AddRange(seededRegistrations);
                        context.AddRange(seededStudentEnrollments);
                        context.AddRange(seededDocuments);
                        context.SaveChanges();
                    }
                }
            }
            return webHost;
        }
    }
}