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
        public async Task<ActionResult> GetEnrollmens()
        {
            var claimsDict = new Dictionary<string, string>();

            HttpContext.User.Claims.ToList()
                .ForEach(_ => claimsDict.Add(_.Type, _.Value));
            
            var userEmail = 
                claimsDict["Email"];
            var userRole =
                claimsDict["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
            var userId =
                int.Parse(claimsDict["UserId"]);

            var enrollments = new List<IEnumerable<StudentEnrollment>>();

            switch(userRole)
            {
                case "Student":

                    enrollments = await _context
                        .Students
                        .Where(_ => _.StudentId == userId)
                        .GetStudentEnrollments()
                        .ToListAsync();

                    return Ok(enrollments);

                // TODO return only enrollments for any registration in which the student ids are
                // contained within student enrollments linked to a registration that an instructor owns
                case "Instructor":
                    break;

                case "Admin":

                    enrollments = await _context
                        .Students
                        .GetStudentEnrollments()
                        .ToListAsync();

                    return Ok(enrollments);
            }
            return Unauthorized();
        }

        // TODO for admin can get any student enrollment, for student can only
        // get the enrollment if they posess the enrollment
        [HttpGet("{studentId}"), Authorize(Roles = "Student, Admin")]
        public async Task<ActionResult> GetEnrollmentsById(int studentId)
        {
            var enrollments = await _context
                .Students
                .Where(_ => _.StudentId == studentId)
                .GetStudentEnrollments()
                .ToListAsync();

            return Ok(enrollments);
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