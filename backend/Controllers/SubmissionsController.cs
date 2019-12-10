using Microsoft.AspNetCore.Mvc;
using backend.Data.Contexts;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using backend.Infrastructure.ClaimsManager;
using System;
using backend.Data.QueryObjects;
using backend.Data.Models;
using backend.Models;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubmissionController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public SubmissionController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("{submissionId}"), Authorize(Roles = "Instructor")]
        public async Task<ActionResult> CreateGrade([FromBody]SubmissionGradeModel newSubmission)
        {
            var claimsManager = new ClaimsManager(HttpContext.User);

            if (ModelState.IsValid)
            {
                var targetSubmission = await _context
                        .Submissions
                        .Where(_ => _.SubmissionId == newSubmission.submissionId)
                        .FirstOrDefaultAsync();

                if (targetSubmission != null)
                {
                    targetSubmission.Grade = newSubmission.grade;
                    _context.Submissions.Update(targetSubmission);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                return BadRequest();
            }
            return BadRequest();
        }

        [HttpPost, Authorize(Roles = "Student")]
        public async Task<ActionResult> CreateSubmission(Submission newSubmission)
        {
            var claimsManager = new ClaimsManager(HttpContext.User);

            if (ModelState.IsValid)
            {

                if (claimsManager.GetRoleClaim() == "Student")
                {
                    var targetDocument = await _context
                        .Documents
                        .Where(_ => _.DocumentId == newSubmission.DocumentId)
                        .FirstOrDefaultAsync();

                    var targetEnrollment = await _context
                        .StudentEnrollment
                        .Where(_ => _.StudentId == claimsManager.GetUserIdClaim())
                        .Where(_ => _.RegistrationId == targetDocument.RegistrationId)
                        .FirstOrDefaultAsync();

                    if (targetEnrollment == null || targetEnrollment.StudentId != claimsManager.GetUserIdClaim())
                        return Unauthorized();

                    if (targetDocument == null)
                    {
                        ModelState.AddModelError("Errors", "That document could not be found");
                        return BadRequest(ModelState);
                    }

                    newSubmission.SubmissionTime = DateTime.UtcNow;
                    newSubmission.StudentEnrollmentId = targetEnrollment.StudentEnrollmentId;
                    await _context.AddAsync(newSubmission);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                return Unauthorized();
            }
            return Ok();
        }

        [HttpGet("{documentId}"), Authorize(Roles = "Instructor, Student")]
        public async Task<ActionResult> Get(int documentId)
        {
            var claimsManager = new ClaimsManager(HttpContext.User);

            // TODO move all this into a query object
            var targetDocument = await _context
                .Documents
                .GetDocumentsWithSubmissions(documentId)
                .FirstOrDefaultAsync();

            if (claimsManager.GetRoleClaim() == "Instructor")
            {
                if (targetDocument.Registration.InstructorId == claimsManager.GetUserIdClaim())
                {
                    return Ok(targetDocument.Submissions);
                }
                return Unauthorized();
            }

            if (claimsManager.GetRoleClaim() == "Student")
            {
                var studentSubmission = targetDocument.Submissions.Where(_ => _.StudentEnrollment.StudentId == claimsManager.GetUserIdClaim());
                if (studentSubmission != null)
                {
                    return Ok(studentSubmission);
                }
                return Unauthorized();
            }

            return Unauthorized();
        }
    }
}