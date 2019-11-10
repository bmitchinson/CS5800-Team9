using System.Linq;
using Microsoft.AspNetCore;
using backend.Data.Models;
using backend.Data.Contexts;
using System.Collections.Generic;

namespace backend.Data.QueryObjects
{
    public static class RegistrationQuery
    {
        public static IQueryable<ICollection<Registration>> GetRegistrations(
        this IQueryable<Instructor> instructors) =>
        instructors
        .Select(_ => _.Registrations
            .Select(reg => new Registration
            {
                RegistrationId = reg.RegistrationId,
                CourseId = reg.CourseId,
                InstructorId = reg.InstructorId,
                EnrollmentLimit = reg.EnrollmentLimit,
                Course = new Course
                {
                    CourseId = reg.Course.CourseId,
                    CourseName = reg.Course.CourseName,
                    CreditHours = reg.Course.CreditHours,
                    Section = reg.Course.Section,
                    StartTime = reg.Course.StartTime,
                    EndTime = reg.Course.EndTime,
                    Level = reg.Course.Level,
                    Prerequisites = reg.Course.Prerequisites
                        .Select(pre => new Prerequisite
                        {
                            CourseId = pre.CourseId,
                            PrerequisiteId = pre.PrerequisiteId,
                            IsMandatory = pre.IsMandatory
                        })
                        .ToList()
                },
                Instructor = new Instructor
                {
                    InstructorId = reg.Instructor.InstructorId,
                    FirstName = reg.Instructor.FirstName,
                    LastName = reg.Instructor.LastName,
                    Email = reg.Instructor.Email
                }
            }).ToList());
    }
}