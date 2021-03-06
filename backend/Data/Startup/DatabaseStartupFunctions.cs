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
                                Password = PasswordSecurity.HashPassword("secret"),
                                EmailConfirmed = true
                            },
                            new Student
                            {
                                StudentId = 2,
                                FirstName = "John",
                                LastName = "Smith",
                                BirthDate = new DateTime(1997, 7, 23),
                                Email = "email2@test.com",
                                Password = PasswordSecurity.HashPassword("secret"),
                                EmailConfirmed = true
                            },
                            new Student
                            {
                                StudentId = 3,
                                FirstName = "Laura",
                                LastName = "Jackson",
                                BirthDate = new DateTime(2001, 1, 13),
                                Email = "email3@test.com",
                                Password = PasswordSecurity.HashPassword("secret"),
                                EmailConfirmed = true
                            },
                            new Student
                            {
                                StudentId = 4,
                                FirstName = "Ben",
                                LastName = "Mitchinson",
                                BirthDate = new DateTime(2001, 1, 13),
                                Email = "email4@test.com",
                                Password = PasswordSecurity.HashPassword("secret"),
                                EmailConfirmed = true
                            },
                            new Student
                            {
                                StudentId = 5,
                                FirstName = "Jacob",
                                LastName = "Watters",
                                BirthDate = new DateTime(2001, 1, 13),
                                Email = "email5@test.com",
                                Password = PasswordSecurity.HashPassword("secret"),
                                EmailConfirmed = true
                            },
                            new Student
                            {
                                StudentId = 6,
                                FirstName = "Greg",
                                LastName = "Mich",
                                BirthDate = new DateTime(2001, 1, 13),
                                Email = "email6@test.com",
                                Password = PasswordSecurity.HashPassword("secret"),
                                EmailConfirmed = true
                            },
                            new Student
                            {
                                StudentId = 7,
                                FirstName = "Alex",
                                LastName = "Powers",
                                BirthDate = new DateTime(2001, 1, 13),
                                Email = "email7@test.com",
                                Password = PasswordSecurity.HashPassword("secret"),
                                EmailConfirmed = true
                            },
                            new Student
                            {
                                StudentId = 8,
                                FirstName = "Nick",
                                LastName = "Grove",
                                BirthDate = new DateTime(2001, 1, 13),
                                Email = "email8@test.com",
                                Password = PasswordSecurity.HashPassword("secret"),
                                EmailConfirmed = true
                            },
                            new Student
                            {
                                StudentId = 9,
                                FirstName = "Griffin",
                                LastName = "Fox",
                                BirthDate = new DateTime(2001, 1, 13),
                                Email = "email9@test.com",
                                Password = PasswordSecurity.HashPassword("secret"),
                                EmailConfirmed = true
                            },
                            new Student
                            {
                                StudentId = 10,
                                FirstName = "Matt",
                                LastName = "Stogs",
                                BirthDate = new DateTime(2001, 1, 13),
                                Email = "email10@test.com",
                                Password = PasswordSecurity.HashPassword("secret"),
                                EmailConfirmed = true
                            },
                            new Student
                            {
                                StudentId = 11,
                                FirstName = "Mia",
                                LastName = "Mitchinson",
                                BirthDate = new DateTime(2001, 1, 13),
                                Email = "email11@test.com",
                                Password = PasswordSecurity.HashPassword("secret"),
                                EmailConfirmed = true
                            },
                            new Student
                            {
                                StudentId = 12,
                                FirstName = "Quincy",
                                LastName = "Jones",
                                BirthDate = new DateTime(2001, 1, 13),
                                Email = "email12@test.com",
                                Password = PasswordSecurity.HashPassword("secret"),
                                EmailConfirmed = true
                            },
                            new Student
                            {
                                StudentId = 13,
                                FirstName = "Jilly",
                                LastName = "William",
                                BirthDate = new DateTime(2001, 1, 13),
                                Email = "email13@test.com",
                                Password = PasswordSecurity.HashPassword("secret"),
                                EmailConfirmed = true
                            },
                            new Student
                            {
                                StudentId = 14,
                                FirstName = "Meghan",
                                LastName = "Nance",
                                BirthDate = new DateTime(2001, 1, 13),
                                Email = "email14@test.com",
                                Password = PasswordSecurity.HashPassword("secret"),
                                EmailConfirmed = true
                            },
                            new Student
                            {
                                StudentId = 15,
                                FirstName = "Olivia",
                                LastName = "Sandvold",
                                BirthDate = new DateTime(2001, 1, 13),
                                Email = "email15@test.com",
                                Password = PasswordSecurity.HashPassword("secret"),
                                EmailConfirmed = true
                            }
                        };

                        var seededInstructors = new List<Instructor>
                        {
                            new Instructor
                            {
                                InstructorId = 1,
                                FirstName = "Jackson",
                                LastName = "Crawford",
                                BirthDate = new DateTime(1975, 1, 13),
                                Email = "jcrawford@test.com",
                                Password = PasswordSecurity.HashPassword("secret"),
                                EmailConfirmed = true
                            },
                            new Instructor
                            {
                                InstructorId = 2,
                                FirstName = "Maggie",
                                LastName = "Ellis",
                                BirthDate = new DateTime(1975, 1, 13),
                                Email = "mellis@test.com",
                                Password = PasswordSecurity.HashPassword("secret"),
                                EmailConfirmed = true
                            },
                            new Instructor
                            {
                                InstructorId = 3,
                                FirstName = "Alex",
                                LastName = "Smith",
                                BirthDate = new DateTime(1975, 1, 13),
                                Email = "smith@test.com",
                                Password = PasswordSecurity.HashPassword("secret"),
                                EmailConfirmed = true
                            },
                            new Instructor
                            {
                                InstructorId = 4,
                                FirstName = "Hans",
                                LastName = "Johnson",
                                BirthDate = new DateTime(1975, 1, 13),
                                Email = "johnson@test.com",
                                Password = PasswordSecurity.HashPassword("secret"),
                                EmailConfirmed = true
                            },
                            new Instructor
                            {
                                InstructorId = 5,
                                FirstName = "John",
                                LastName = "Lim",
                                BirthDate = new DateTime(1975, 1, 13),
                                Email = "lim@test.com",
                                Password = PasswordSecurity.HashPassword("secret"),
                                EmailConfirmed = true
                            },
                            new Instructor
                            {
                                InstructorId = 6,
                                FirstName = "Lisa",
                                LastName = "Dorothy",
                                BirthDate = new DateTime(1975, 1, 13),
                                Email = "dorothy@test.com",
                                Password = PasswordSecurity.HashPassword("secret"),
                                EmailConfirmed = true
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
                                CreditHours = 4
                            },
                            new Course
                            {
                                CourseId = 2,
                                CourseName = "Chemistry II",
                                CreditHours = 4
                            },
                            new Course
                            {
                                CourseId = 3,
                                CourseName = "Fundamentals of Software Engineering",
                                CreditHours = 3
                            },
                            new Course
                            {
                                CourseId = 4,
                                CourseName = "Physics I",
                                CreditHours = 4
                            },
                            new Course
                            {
                                CourseId = 5,
                                CourseName = "Math II",
                                CreditHours = 3
                            },
                            new Course
                            {
                                CourseId = 6,
                                CourseName = "Physics II",
                                CreditHours = 3
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
                                Section = "CHEM:1110",
                                StartTime = DateTime.Parse("9:00 AM"),
                                EndTime = DateTime.Parse("10:45 AM")
                            },
                            new Registration
                            {
                                RegistrationId = 2,
                                CourseId = 2, // Chem 2
                                InstructorId = 1,
                                EnrollmentLimit = 20,
                                Section = "CHEM:2220",
                                StartTime = DateTime.Parse("2:30 PM"),
                                EndTime = DateTime.Parse("3:20 PM"),
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
                                EnrollmentLimit = 20,
                                Section = "CS:5800:AA",
                                StartTime = DateTime.Parse("5:30 PM"),
                                EndTime = DateTime.Parse("7:00 PM")
                            },
                            new Registration
                            {
                                RegistrationId = 4,
                                CourseId = 3, // SE
                                InstructorId = 3,
                                EnrollmentLimit = 20,
                                Section = "CS:5800:BB",
                                StartTime = DateTime.Parse("5:30 PM"),
                                EndTime = DateTime.Parse("7:00 PM")
                            },
                            new Registration
                            {
                                RegistrationId = 5,
                                CourseId = 4, // Phys 1
                                InstructorId = 4,
                                EnrollmentLimit = 25,
                                Section = "PHYS:1611",
                                StartTime = DateTime.Parse("5:30 PM"),
                                EndTime = DateTime.Parse("7:00 PM")
                            },
                            new Registration
                            {
                                RegistrationId = 6,
                                CourseId = 5, // Math II
                                InstructorId = 5,
                                EnrollmentLimit = 40,
                                Section = "MATH:1560",
                                StartTime = DateTime.Parse("5:30 PM"),
                                EndTime = DateTime.Parse("7:00 PM")
                            },
                            new Registration
                            {
                                RegistrationId = 7,
                                CourseId = 6, // Phys II
                                InstructorId = 6,
                                EnrollmentLimit = 30,
                                Section = "Phys:2611",
                                StartTime = DateTime.Parse("5:30 PM"),
                                EndTime = DateTime.Parse("7:00 PM"),
                                Prerequisites = new List<Prerequisite>
                                {
                                    new Prerequisite
                                    {
                                        CourseId = 4 // Phys I
                                    },
                                    new Prerequisite
                                    {
                                        CourseId = 5 // Math II
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
                                RegistrationId = 1,
                                IsCompleted = true,
                                Grade = "A-"
                            },
                            new StudentEnrollment
                            {
                                StudentId = 1,
                                StudentEnrollmentId = 2,
                                RegistrationId = 3,
                                IsCompleted = true,
                                Grade = "C+"
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

                        var seededSubmissions = new List<Submission>
                        {
                            new Submission {
                                DocumentId = 5,
                                Grade = "B",
                                StudentEnrollmentId = 4,
                                ResourceLink = "1ttps://res.cloudinary.com/dkfj0xfet/image/upload/v1575184547/classroom/seed/chem/Assignment/chem_hw1_bmpymy.pdf"
                            },
                            new Submission {
                                Grade = "A",
                                DocumentId = 5,
                                StudentEnrollmentId = 1,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184547/classroom/seed/chem/Assignment/chem_hw1_bmpymy.pdf"
                            },
                            new Submission {
                                Grade = "B",
                                DocumentId = 6,
                                StudentEnrollmentId = 1,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184547/classroom/seed/chem/Assignment/chem_hw1_bmpymy.pdf"
                            },
                            new Submission {
                                Grade = "D",
                                DocumentId = 7,
                                StudentEnrollmentId = 1,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184547/classroom/seed/chem/Assignment/chem_hw1_bmpymy.pdf"
                            },
                            new Submission {
                                Grade = "A",
                                DocumentId = 8,
                                StudentEnrollmentId = 1,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184547/classroom/seed/chem/Assignment/chem_hw1_bmpymy.pdf"
                            },
                            new Submission {
                                DocumentId = 9,
                                StudentEnrollmentId = 1,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184547/classroom/seed/chem/Assignment/chem_hw1_bmpymy.pdf"
                            }
                        };

                        var seededDocuments = new List<Document>
                        {
                            new Document
                            {
                                DocumentId = 1,
                                RegistrationId = 1,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184589/classroom/seed/chem/Quiz/chem_quiz1_cb0izb.pdf",
                                DocType = DocumentType.Quiz
                            },
                            new Document
                            {
                                DocumentId = 2,
                                RegistrationId = 1,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184589/classroom/seed/chem/Quiz/chem_quiz2_czwcqe.pdf",
                                DocType = DocumentType.Quiz
                            },
                            new Document
                            {
                                DocumentId = 3,
                                RegistrationId = 1,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184589/classroom/seed/chem/Quiz/chem_quiz3_fmfz76.pdf",
                                DocType = DocumentType.Quiz
                            },
                            new Document
                            {
                                DocumentId = 4,
                                RegistrationId = 1,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184589/classroom/seed/chem/Quiz/chem_quiz4_nnwlk9.pdf",
                                DocType = DocumentType.Quiz
                            },
                            new Document
                            {
                                DocumentId = 5,
                                RegistrationId = 1,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184547/classroom/seed/chem/Assignment/chem_hw1_bmpymy.pdf",
                                DocType = DocumentType.Assignment
                            },
                            new Document
                            {
                                DocumentId = 6,
                                RegistrationId = 1,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184547/classroom/seed/chem/Assignment/chem_hw2_ltqv0g.pdf",
                                DocType = DocumentType.Assignment
                            },
                            new Document
                            {
                                DocumentId = 7,
                                RegistrationId = 1,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184547/classroom/seed/chem/Assignment/chem_hw3_bemawp.pdf",
                                DocType = DocumentType.Assignment
                            },
                            new Document
                            {
                                DocumentId = 8,
                                RegistrationId = 1,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184548/classroom/seed/chem/Assignment/chem_hw4_y50oku.pdf",
                                DocType = DocumentType.Assignment
                            },
                            new Document
                            {
                                DocumentId = 9,
                                RegistrationId = 1,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184547/classroom/seed/chem/Assignment/chem_hw5_agrcfz.pdf",
                                DocType = DocumentType.Assignment
                            },
                            new Document
                            {
                                DocumentId = 10,
                                RegistrationId = 1,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184547/classroom/seed/chem/Assignment/chem_hw6_ozgwpw.pdf",
                                DocType = DocumentType.Assignment
                            },
                            new Document
                            {
                                DocumentId = 11,
                                RegistrationId = 1,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184565/classroom/seed/chem/Exam/chem_exam1_xogwft.pdf",
                                DocType = DocumentType.Exam
                            },
                            new Document
                            {
                                DocumentId = 12,
                                RegistrationId = 1,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184565/classroom/seed/chem/Exam/chem_exam2_u5uiec.pdf",
                                DocType = DocumentType.Exam
                            },
                            new Document
                            {
                                DocumentId = 13,
                                RegistrationId = 1,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184565/classroom/seed/chem/Exam/chem_exam3_crg0vb.pdf",
                                DocType = DocumentType.Exam
                            },
                            new Document
                            {
                                DocumentId = 14,
                                RegistrationId = 1,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184565/classroom/seed/chem/Exam/chem_exam_final_l9x6fw.pdf",
                                DocType = DocumentType.Exam
                            },
                            new Document
                            {
                                DocumentId = 15,
                                RegistrationId = 1,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184580/classroom/seed/chem/Notes/chem_chapter6_vjkvpg.pdf",
                                DocType = DocumentType.Notes
                            },
                            new Document
                            {
                                DocumentId = 16,
                                RegistrationId = 1,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184580/classroom/seed/chem/Notes/chem_chapter5_sicssw.pdf",
                                DocType = DocumentType.Notes
                            },
                            new Document
                            {
                                DocumentId = 17,
                                RegistrationId = 1,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184579/classroom/seed/chem/Notes/chem_chapter12_bmkikm.pdf",
                                DocType = DocumentType.Notes
                            },
                            new Document
                            {
                                DocumentId = 18,
                                RegistrationId = 1,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184579/classroom/seed/chem/Notes/chem_syllabus_phgeha.pdf",
                                DocType = DocumentType.Notes
                            },
                            new Document
                            {
                                DocumentId = 19,
                                RegistrationId = 1,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184579/classroom/seed/chem/Notes/chem_practiceexam1_ndfrt8.pdf",
                                DocType = DocumentType.Notes
                            },
                            new Document
                            {
                                DocumentId = 20,
                                RegistrationId = 1,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184579/classroom/seed/chem/Notes/chem_chapter1234_de29ut.pdf",
                                DocType = DocumentType.Notes
                            },
                            new Document
                            {
                                DocumentId = 21,
                                RegistrationId = 1,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184579/classroom/seed/chem/Notes/chem_chapter11_aktut5.pdf",
                                DocType = DocumentType.Notes
                            },
                            new Document
                            {
                                DocumentId = 22,
                                RegistrationId = 1,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184578/classroom/seed/chem/Notes/chem_chapter9_qqremb.pdf",
                                DocType = DocumentType.Notes
                            },
                            new Document
                            {
                                DocumentId = 23,
                                RegistrationId = 1,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184578/classroom/seed/chem/Notes/chem_chapter10_chtw66.pdf",
                                DocType = DocumentType.Notes
                            },
                            new Document
                            {
                                DocumentId = 24,
                                RegistrationId = 1,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184578/classroom/seed/chem/Notes/chem_chapter7_vrernk.pdf",
                                DocType = DocumentType.Notes
                            },
                            new Document
                            {
                                DocumentId = 25,
                                RegistrationId = 1,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184577/classroom/seed/chem/Notes/chem_chapter8_zudu8y.pdf",
                                DocType = DocumentType.Notes
                            },
                            new Document
                            {
                                DocumentId = 26,
                                RegistrationId = 2,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184589/classroom/seed/chem/Quiz/chem_quiz1_cb0izb.pdf",
                                DocType = DocumentType.Quiz
                            },
                            new Document
                            {
                                DocumentId = 27,
                                RegistrationId = 2,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184589/classroom/seed/chem/Quiz/chem_quiz2_czwcqe.pdf",
                                DocType = DocumentType.Quiz
                            },
                            new Document
                            {
                                DocumentId = 28,
                                RegistrationId = 2,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184589/classroom/seed/chem/Quiz/chem_quiz3_fmfz76.pdf",
                                DocType = DocumentType.Quiz
                            },
                            new Document
                            {
                                DocumentId = 29,
                                RegistrationId = 2,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184589/classroom/seed/chem/Quiz/chem_quiz4_nnwlk9.pdf",
                                DocType = DocumentType.Quiz
                            },
                            new Document
                            {
                                DocumentId = 30,
                                RegistrationId = 2,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184547/classroom/seed/chem/Assignment/chem_hw1_bmpymy.pdf",
                                DocType = DocumentType.Assignment
                            },
                            new Document
                            {
                                DocumentId = 31,
                                RegistrationId = 2,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184547/classroom/seed/chem/Assignment/chem_hw2_ltqv0g.pdf",
                                DocType = DocumentType.Assignment
                            },
                            new Document
                            {
                                DocumentId = 32,
                                RegistrationId = 2,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184547/classroom/seed/chem/Assignment/chem_hw3_bemawp.pdf",
                                DocType = DocumentType.Assignment
                            },
                            new Document
                            {
                                DocumentId = 33,
                                RegistrationId = 2,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184548/classroom/seed/chem/Assignment/chem_hw4_y50oku.pdf",
                                DocType = DocumentType.Assignment
                            },
                            new Document
                            {
                                DocumentId = 34,
                                RegistrationId = 2,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184547/classroom/seed/chem/Assignment/chem_hw5_agrcfz.pdf",
                                DocType = DocumentType.Assignment
                            },
                            new Document
                            {
                                DocumentId = 35,
                                RegistrationId = 2,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184547/classroom/seed/chem/Assignment/chem_hw6_ozgwpw.pdf",
                                DocType = DocumentType.Assignment
                            },
                            new Document
                            {
                                DocumentId = 36,
                                RegistrationId = 2,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184565/classroom/seed/chem/Exam/chem_exam1_xogwft.pdf",
                                DocType = DocumentType.Exam
                            },
                            new Document
                            {
                                DocumentId = 37,
                                RegistrationId = 2,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184565/classroom/seed/chem/Exam/chem_exam2_u5uiec.pdf",
                                DocType = DocumentType.Exam
                            },
                            new Document
                            {
                                DocumentId = 38,
                                RegistrationId = 2,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184565/classroom/seed/chem/Exam/chem_exam3_crg0vb.pdf",
                                DocType = DocumentType.Exam
                            },
                            new Document
                            {
                                DocumentId = 39,
                                RegistrationId = 2,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184565/classroom/seed/chem/Exam/chem_exam_final_l9x6fw.pdf",
                                DocType = DocumentType.Exam
                            },
                            new Document
                            {
                                DocumentId = 40,
                                RegistrationId = 2,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184580/classroom/seed/chem/Notes/chem_chapter6_vjkvpg.pdf",
                                DocType = DocumentType.Notes
                            },
                            new Document
                            {
                                DocumentId = 41,
                                RegistrationId = 2,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184579/classroom/seed/chem/Notes/chem_chapter12_bmkikm.pdf",
                                DocType = DocumentType.Notes
                            },
                            new Document
                            {
                                DocumentId = 42,
                                RegistrationId = 2,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184579/classroom/seed/chem/Notes/chem_syllabus_phgeha.pdf",
                                DocType = DocumentType.Notes
                            },
                            new Document
                            {
                                DocumentId = 43,
                                RegistrationId = 2,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184579/classroom/seed/chem/Notes/chem_practiceexam1_ndfrt8.pdf",
                                DocType = DocumentType.Notes
                            },
                            new Document
                            {
                                DocumentId = 44,
                                RegistrationId = 2,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184579/classroom/seed/chem/Notes/chem_chapter1234_de29ut.pdf",
                                DocType = DocumentType.Notes
                            },
                            new Document
                            {
                                DocumentId = 45,
                                RegistrationId = 2,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184579/classroom/seed/chem/Notes/chem_chapter15_dodxds.pdf",
                                DocType = DocumentType.Notes
                            },
                            new Document
                            {
                                DocumentId = 46,
                                RegistrationId = 2,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184579/classroom/seed/chem/Notes/chem_chapter11_aktut5.pdf",
                                DocType = DocumentType.Notes
                            },
                            new Document
                            {
                                DocumentId = 47,
                                RegistrationId = 2,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184578/classroom/seed/chem/Notes/chem_chapter9_qqremb.pdf",
                                DocType = DocumentType.Notes
                            },
                            new Document
                            {
                                DocumentId = 48,
                                RegistrationId = 2,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184578/classroom/seed/chem/Notes/chem_chapter10_chtw66.pdf",
                                DocType = DocumentType.Notes
                            },
                            new Document
                            {
                                DocumentId = 49,
                                RegistrationId = 2,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184578/classroom/seed/chem/Notes/chem_chapter7_vrernk.pdf",
                                DocType = DocumentType.Notes
                            },
                            new Document
                            {
                                DocumentId = 50,
                                RegistrationId = 2,
                                ResourceLink = "https://res.cloudinary.com/dkfj0xfet/image/upload/v1575184577/classroom/seed/chem/Notes/chem_chapter8_zudu8y.pdf",
                                DocType = DocumentType.Notes
                            }
                        };

                        context.AddRange(seededStudents);
                        context.AddRange(seededCourses);
                        context.AddRange(seededInstructors);
                        context.AddRange(seededAdmins);
                        context.AddRange(seededRegistrations);
                        context.AddRange(seededStudentEnrollments);
                        context.AddRange(seededDocuments);
                        context.AddRange(seededSubmissions);
                        context.SaveChanges();
                    }
                }
            }
            return webHost;
        }
    }
}