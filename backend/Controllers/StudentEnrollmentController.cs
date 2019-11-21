using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using backend.Data.Contexts;
using backend.Data.Models;
using Microsoft.EntityFrameworkCore;
using backend.Data.QueryObjects;
using backend.Infrastructure.ClaimsManager;
using System.Web;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentEnrollmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentEnrollmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // TODO for admin should be able to get all roles, for instructor should be
        // only able to get enrollments for any registrations that they own, and for
        // students they can get the enrollments in which they own
        [HttpGet, Authorize(Roles = "Admin, Instructor, Student")]
        public async Task<ActionResult> GetEnrollments()
        {
            var claimsManager = new ClaimsManager(HttpContext.User);

            var enrollments = new List<IEnumerable<StudentEnrollment>>();

            switch(claimsManager.GetRoleClaim())
            {
                case "Student":

                    enrollments = await _context
                        .Students
                        .Where(_ => _.StudentId == claimsManager.GetUserIdClaim())
                        .GetStudentEnrollmentsFromStudent()
                        .ToListAsync();

                    return Ok(enrollments);

                // TODO move this out of the controller into a static method or something
                // to reduce clutter

                // BUG this returns enrollments for the student with a different instructor
                case "Instructor":
                    var studentIds = await _context
                        .Registrations
                        .Where(_ => _.InstructorId == claimsManager.GetUserIdClaim())
                        .Select(_ => _.StudentEnrollments
                            .Select(s => s.StudentId))
                        .FirstOrDefaultAsync();

                    var registrationIds = await _context
                        .Registrations
                        .Where(_ => _.InstructorId == claimsManager.GetUserIdClaim())
                        .Select(_ => _.StudentEnrollments
                            .Select(s => s.RegistrationId))
                        .FirstOrDefaultAsync();

                    foreach (int studentId in studentIds)
                    {
                        var selectedStudentEnrollment = await _context
                            .Students
                            .Where(_ => _.StudentId == studentId)
                            .GetStudentEnrollmentsFromStudent()
                            .FirstOrDefaultAsync();

                        enrollments.Add(selectedStudentEnrollment);
                    }
                    return Ok(enrollments);

                case "Admin":

                    enrollments = await _context
                        .Students
                        .GetStudentEnrollmentsFromStudent()
                        .ToListAsync();

                    return Ok(enrollments);
            }
            return Unauthorized();
        }

        // TODO for admin can get any student enrollment, for student can only
        // get the enrollment if they posess the enrollment, for teacher can only get enrollment
        // if it within a registration they own
        [HttpGet("{studentId}"), Authorize(Roles = "Student, Admin, Instructor")]
        public async Task<ActionResult> GetEnrollmentsById(int studentId)
        {
            var claimsManager = new ClaimsManager(HttpContext.User);

            var enrollments = new List<IEnumerable<StudentEnrollment>>();

            switch (claimsManager.GetRoleClaim())
            {
                case "Student":
                    if (claimsManager.GetUserIdClaim() != studentId)
                    {
                        return Unauthorized();
                    }
                    
                    enrollments = await _context
                        .Students
                        .Where(_ => _.StudentId == studentId)
                        .GetStudentEnrollmentsFromStudent()
                        .ToListAsync();
                    
                    return Ok(enrollments);

                // BUG this returns student enrolmments for instructors that are not the
                // current instructor, needs to be fixed
                case "Instructor":

                    var studentIds = await _context
                        .Registrations
                        .Where(_ => _.InstructorId == claimsManager.GetUserIdClaim())
                        .Select(_ => _.StudentEnrollments
                            .Select(s => s.StudentId))
                        .FirstOrDefaultAsync();
                    
                    if (studentIds.Contains(studentId))
                    {
                        foreach (int id in studentIds)
                        {
                            if (id == studentId)
                            {
                                var selectedStudentEnrollment = await _context
                                .Students
                                .Where(_ => _.StudentId == id)
                                .GetStudentEnrollmentsFromStudent()
                                .FirstOrDefaultAsync();
                                enrollments.Add(selectedStudentEnrollment);
                            }
                        }
                        return Ok(enrollments);
                    }
                    
                    return Unauthorized();

                case "Admin":

                    enrollments = await _context
                        .Students
                        .Where(_ => _.StudentId == studentId)
                        .GetStudentEnrollmentsFromStudent()
                        .ToListAsync();

                    return Ok(enrollments);
            }

            return Unauthorized();
        }

        // TODO student should be able to enroll into a course provided they meet the prereqs and
        // the enrollment limit is not exceeded
        [HttpPost, Authorize(Roles = "Student, Admin")]
        public async Task<ActionResult> Enroll([FromBody]StudentEnrollment studentEnrollment)
        {

            return Ok();
        }

        // TODO student should be able to unenroll from a course
        [HttpDelete, Authorize(Roles = "Student, Admin")]
        public async Task<ActionResult> Unenroll(int id)
        {
            return Ok();
        }
    }
} 