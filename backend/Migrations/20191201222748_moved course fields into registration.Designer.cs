﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using backend.Data.Contexts;

namespace backend.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20191201222748_moved course fields into registration")]
    partial class movedcoursefieldsintoregistration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("backend.Data.Models.Administrator", b =>
                {
                    b.Property<int>("AdministratorId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.HasKey("AdministratorId");

                    b.ToTable("Administrators");
                });

            modelBuilder.Entity("backend.Data.Models.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CourseName")
                        .IsRequired();

                    b.Property<int>("CreditHours");

                    b.Property<string>("Level")
                        .IsRequired();

                    b.Property<bool>("SoftDeleted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.HasKey("CourseId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("backend.Data.Models.Document", b =>
                {
                    b.Property<int>("DocumentId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DocType")
                        .IsRequired();

                    b.Property<int>("RegistrationId");

                    b.Property<string>("ResourceLink")
                        .IsRequired();

                    b.HasKey("DocumentId");

                    b.HasIndex("RegistrationId");

                    b.ToTable("Document");
                });

            modelBuilder.Entity("backend.Data.Models.Instructor", b =>
                {
                    b.Property<int>("InstructorId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.HasKey("InstructorId");

                    b.ToTable("Instructors");
                });

            modelBuilder.Entity("backend.Data.Models.Prerequisite", b =>
                {
                    b.Property<int>("PrerequisiteId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CourseId");

                    b.Property<bool>("IsMandatory");

                    b.Property<int?>("RegistrationId");

                    b.HasKey("PrerequisiteId");

                    b.HasIndex("CourseId");

                    b.HasIndex("RegistrationId");

                    b.ToTable("Prerequisite");
                });

            modelBuilder.Entity("backend.Data.Models.Registration", b =>
                {
                    b.Property<int>("RegistrationId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CourseId")
                        .IsRequired();

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime");

                    b.Property<int>("EnrollmentLimit");

                    b.Property<int?>("InstructorId")
                        .IsRequired();

                    b.Property<string>("Section")
                        .IsRequired();

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime");

                    b.HasKey("RegistrationId");

                    b.HasIndex("CourseId");

                    b.HasIndex("InstructorId");

                    b.ToTable("Registrations");
                });

            modelBuilder.Entity("backend.Data.Models.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("BirthDate")
                        .IsRequired()
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.HasKey("StudentId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("backend.Data.Models.StudentEnrollment", b =>
                {
                    b.Property<int>("StudentEnrollmentId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Grade");

                    b.Property<bool>("IsCompleted");

                    b.Property<int>("RegistrationId");

                    b.Property<int>("StudentId");

                    b.HasKey("StudentEnrollmentId");

                    b.HasIndex("RegistrationId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentEnrollment");
                });

            modelBuilder.Entity("backend.Data.Models.Submission", b =>
                {
                    b.Property<int>("SubmissionId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DocumentId");

                    b.Property<string>("Grade");

                    b.Property<string>("ResourceLink");

                    b.Property<int>("StudentEnrollmentId");

                    b.HasKey("SubmissionId");

                    b.HasIndex("DocumentId");

                    b.HasIndex("StudentEnrollmentId");

                    b.ToTable("Submission");
                });

            modelBuilder.Entity("backend.Data.Models.Document", b =>
                {
                    b.HasOne("backend.Data.Models.Registration", "Registration")
                        .WithMany("Documents")
                        .HasForeignKey("RegistrationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("backend.Data.Models.Prerequisite", b =>
                {
                    b.HasOne("backend.Data.Models.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("backend.Data.Models.Registration")
                        .WithMany("Prerequisites")
                        .HasForeignKey("RegistrationId");
                });

            modelBuilder.Entity("backend.Data.Models.Registration", b =>
                {
                    b.HasOne("backend.Data.Models.Course", "Course")
                        .WithMany("Registrations")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("backend.Data.Models.Instructor", "Instructor")
                        .WithMany("Registrations")
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("backend.Data.Models.StudentEnrollment", b =>
                {
                    b.HasOne("backend.Data.Models.Registration", "Registration")
                        .WithMany("StudentEnrollments")
                        .HasForeignKey("RegistrationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("backend.Data.Models.Student", "Student")
                        .WithMany("Enrollments")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("backend.Data.Models.Submission", b =>
                {
                    b.HasOne("backend.Data.Models.Document", "Document")
                        .WithMany("Submissions")
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("backend.Data.Models.StudentEnrollment", "StudentEnrollment")
                        .WithMany("Submissions")
                        .HasForeignKey("StudentEnrollmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
