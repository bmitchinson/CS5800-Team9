using System.Linq;
using Microsoft.EntityFrameworkCore;
using backend.Data.Contexts;
using backend.Data.Models;
using System.Collections.Generic;

namespace backend.Data.QueryObjects
{
    public static class GetCourse
    {
        public static IQueryable<Course> GetCourses(
            this IQueryable<Course> courses) =>
                courses
                .Select(c => new Course
                {
                    CourseId = c.CourseId,
                    CourseName = c.CourseName,
                    CreditHours = c.CreditHours,
                    Section = c.Section,
                    StartTime = c.StartTime,
                    EndTime = c.EndTime,
                    Level = c.Level,
                    SoftDeleted = c.SoftDeleted,
                    Registrations = c.Registrations
                        .Select(reg => new Registration
                        {
                            RegistrationId = reg.RegistrationId,
                            CourseId = reg.CourseId,
                            InstructorId = reg.InstructorId,
                            EnrollmentLimit = reg.EnrollmentLimit,
                            Instructor = new Instructor
                            {
                                InstructorId = reg.Instructor.InstructorId,
                                FirstName = reg.Instructor.FirstName,
                                LastName = reg.Instructor.LastName,
                                Email = reg.Instructor.Email
                            },
                            Prerequisites = reg.Prerequisites
                                .Select(pre => new Prerequisite
                                {
                                    CourseId = pre.CourseId,
                                    PrerequisiteId = pre.PrerequisiteId,
                                    IsMandatory = pre.IsMandatory,
                                    Course = new Course
                                    {
                                        CourseId = pre.Course.CourseId,
                                        CourseName = pre.Course.CourseName,
                                        CreditHours = pre.Course.CreditHours,
                                        Section = pre.Course.Section,
                                        StartTime = pre.Course.StartTime,
                                        EndTime = pre.Course.EndTime,
                                        Level = pre.Course.Level,
                                        SoftDeleted = pre.Course.SoftDeleted
                                    }
                                }).ToList()
                        }).ToList()
                });

        public static IQueryable<Course> FilterSoftDeleted(this IQueryable<Course> courses) =>
            courses
            .Where(_ => !_.SoftDeleted);
    }
}