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

                case "Instructor":

                    enrollments = await _context
                        .Instructors
                        .GetStudentEnrollmentFromInstructor(claimsManager.GetUserIdClaim())
                        .ToListAsync();

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
        public async Task<ActionResult> GetEnrollmentsById(int enrollmentId)
        {
            var claimsManager = new ClaimsManager(HttpContext.User);

            // TODO move this into a query object and load related data that is needed.
            var enrollment = await _context
                .StudentEnrollment
                .Where(_ => _.StudentEnrollmentId == enrollmentId)
                .FirstOrDefaultAsync();

            var enrollments = new List<StudentEnrollment>();

            switch (claimsManager.GetRoleClaim())
            {
                case "Student":

                    if (enrollment.StudentEnrollmentId == claimsManager.GetUserIdClaim())
                    {
                        return Ok(enrollment);
                    }

                    return Unauthorized();


                // TODO this will probably not return correctly because the related data is not yet
                // loaded.
                case "Instructor":

                    if (enrollment.Registration.InstructorId == claimsManager.GetUserIdClaim())
                    {
                        return Ok(enrollment);
                    }

                    return Unauthorized();

                case "Admin":
                    return Ok(enrollment);
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