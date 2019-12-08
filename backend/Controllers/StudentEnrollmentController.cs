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

            switch (claimsManager.GetRoleClaim())
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
        [HttpGet("{enrollmentId}"), Authorize(Roles = "Student, Admin, Instructor")]
        public async Task<ActionResult> GetEnrollmentsById(int enrollmentId)
        {
            var claimsManager = new ClaimsManager(HttpContext.User);

            // TODO move this into a query object and load related data that is needed.
            var enrollment = await _context
                .StudentEnrollment
                .GetStudentEnrollment()
                .Where(_ => _.StudentEnrollmentId == enrollmentId)
                .FirstOrDefaultAsync();

            var enrollments = new List<StudentEnrollment>();

            if (enrollment == null) { return NotFound(); }

            switch (claimsManager.GetRoleClaim())
            {
                case "Student":

                    if (enrollment.StudentId == claimsManager.GetUserIdClaim())
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

        // TODO still need to check pre reqs and still need to ensure that a student cant register
        // for the same registration more than once, additionally may want to constrain the ability to
        // register for the same course twice (of a different registration)
        [HttpPost, Authorize(Roles = "Student, Admin")]
        public async Task<ActionResult> Enroll([FromBody]StudentEnrollment studentEnrollment)
        {
            if (ModelState.IsValid)
            {
                var claimsManager = new ClaimsManager(HttpContext.User);

                if (claimsManager.GetRoleClaim() == "Student")
                {
                    if (studentEnrollment.StudentId != claimsManager.GetUserIdClaim())
                    {
                        return Unauthorized();
                    }
                }
                var targetStudent = await _context
                    .Students
                    .Where(_ => _.StudentId == studentEnrollment.StudentId)
                    .FirstOrDefaultAsync();

                var targetRegistration = await _context
                    .Registrations
                    .Where(_ => _.RegistrationId == studentEnrollment.RegistrationId)
                    .FirstOrDefaultAsync();

                if (targetStudent != null && targetRegistration != null)
                {
                    var newEnrollment = new StudentEnrollment()
                    {
                        Registration = targetRegistration,
                        Student = targetStudent
                    };

                    // Checking for existing enrollment

                    var enrollmentExists = await _context
                        .StudentEnrollment
                        .Where(_ => _.RegistrationId == targetRegistration.RegistrationId
                                && _.StudentId == targetStudent.StudentId)
                        .AnyAsync();

                    if (enrollmentExists)
                    {
                        ModelState.AddModelError("Errors", "You are already enrolled in this course");
                        return BadRequest(ModelState);
                    }

                    // verify that prereqs are met

                    var preRequesites = await _context
                        .Registrations
                        .Where(_ => _.RegistrationId == studentEnrollment.RegistrationId)
                        .Select(_ => _.Prerequisites)
                        .FirstOrDefaultAsync();

                    var completedCourses = await _context
                        .StudentEnrollment
                        .Where(_ => _.StudentId == studentEnrollment.StudentId)
                        .QueryCompletedCourses()
                        .ToListAsync();

                    foreach (StudentEnrollment completedCourse in completedCourses)
                    {
                        if(!preRequesites
                            .Where(_ => _.CourseId == completedCourse.Registration.Course.CourseId)
                            .Any())
                            {
                                ModelState.AddModelError("Errors", "You do not meet the prerequesites for this course");
                                return BadRequest(ModelState);
                            }
                    }

                    if (_context
                        .Registrations
                        .Where(_ => _.RegistrationId == targetRegistration.RegistrationId)
                        .Select(_ => _.StudentEnrollments)
                        .Count() < targetRegistration.EnrollmentLimit)
                    {
                        _context.Add(newEnrollment);
                        _context.SaveChanges();
                        return CreatedAtAction(
                                nameof(GetEnrollmentsById),
                                new { enrollmentId = newEnrollment.StudentEnrollmentId },
                                newEnrollment);
                    }
                    ModelState.AddModelError("Errors", "The enrollment limit has been reached");
                    return BadRequest(ModelState);
                }
                ModelState.AddModelError("Errors", "Student or Registration not found");
                return BadRequest(ModelState);
            }
            return BadRequest(ModelState);
        }

        // TODO student should be able to unenroll from a course
        [HttpDelete("{enrollmentId}"), Authorize(Roles = "Student, Admin")]
        public async Task<ActionResult> Unenroll(int enrollmentId)
        {
            var claimsManager = new ClaimsManager(HttpContext.User);

            var targetEnrollment = await _context
                .StudentEnrollment
                .GetStudentEnrollment()
                .Where(_ => _.StudentEnrollmentId == enrollmentId)
                .FirstOrDefaultAsync();

            if (targetEnrollment != null)
            {
                if (claimsManager.GetRoleClaim() == "Student")
                {
                    if (targetEnrollment.StudentId != claimsManager.GetUserIdClaim())
                    {
                        return Unauthorized();
                    }
                }
                _context.Remove(targetEnrollment);
                _context.SaveChanges();
                return NoContent();
            }
            return NotFound();
        }
    }
}