using backend.Data.Contexts;
using backend.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using backend.Infrastructure.PasswordSecurity;

namespace backend_tests
{
    public static class InMemoryDatabaseStartupHelpers
    {
        public static async Task TestingSeedDatabaseThreeStudentsAsync(this ApplicationDbContext context)
        {
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
                                CourseName = "Database Systems",
                                CreditHours = 3,
                                Section = "00AA",
                                StartTime = DateTime.Parse("9:00 AM"),
                                EndTime = DateTime.Parse("10:45 AM"),
                                Prerequisites = new List<Prerequisite>
                                {
                                    new Prerequisite
                                    {
                                        CourseId = 2
                                    }
                                }
                            },
                            new Course
                            {
                                CourseId = 2,
                                CourseName = "Data Structures",
                                CreditHours = 4,
                                Section = "00BB",
                                StartTime = DateTime.Parse("2:30 PM"),
                                EndTime = DateTime.Parse("3:20 PM")
                            },
                            new Course
                            {
                                CourseId = 3,
                                CourseName = "Software Engineering",
                                CreditHours = 4,
                                Section = "00AA",
                                StartTime = DateTime.Parse("5:30 PM"),
                                EndTime = DateTime.Parse("7:00 PM"),
                                Prerequisites = new List<Prerequisite>
                                {
                                    new Prerequisite
                                    {
                                        CourseId = 1
                                    }
                                }
                            }
                        };

                        var seededRegistrations = new List<Registration>
                        {
                            new Registration
                            {
                                RegistrationId = 1,
                                CourseId = 1,
                                InstructorId = 1,
                                EnrollmentLimit = 40,
                            },
                            new Registration
                            {
                                RegistrationId = 2,
                                CourseId = 2,
                                InstructorId = 1,
                                EnrollmentLimit = 30
                            },
                            new Registration
                            {
                                RegistrationId = 3,
                                CourseId = 3,
                                InstructorId = 2,
                                EnrollmentLimit = 20
                            }
                        };

                        var seededStudentEnrollments = new List<StudentEnrollment>
                        {
                            new StudentEnrollment
                            {
                                StudentId = 1,
                                RegistrationId = 1
                            },
                            new StudentEnrollment
                            {
                                StudentId = 1,
                                RegistrationId = 2
                            },
                            new StudentEnrollment
                            {
                                StudentId = 2,
                                RegistrationId = 1
                            },
                            new StudentEnrollment
                            {
                                StudentId = 3,
                                RegistrationId = 3
                            }
                        };

                        await context.AddRangeAsync(seededStudents);
                        await context.AddRangeAsync(seededCourses);
                        await context.AddRangeAsync(seededInstructors);
                        await context.AddRangeAsync(seededAdmins);
                        await context.AddRangeAsync(seededRegistrations);
                        await context.AddRangeAsync(seededStudentEnrollments);
                        await context.SaveChangesAsync();
        }
    }
}