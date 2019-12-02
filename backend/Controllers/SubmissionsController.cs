using Microsoft.AspNetCore.Mvc;
using backend.Data.Contexts;
using backend.Data.Models;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using backend.Infrastructure.ClaimsManager;

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

        [HttpPost, Authorize(Roles = "Student")]
        public async Task<ActionResult> CreateSubmission(Submission newSubmission)
        {
            var claimsManager = new ClaimsManager(HttpContext.User);

            if (ModelState.IsValid)
            {
                
                if (claimsManager.GetRoleClaim() == "Student")
                {
                    var targetDocument = await _context
                        .Registrations
                        .Select(_ => _.Documents
                            .Where(doc => doc.DocumentId == newSubmission.DocumentId))
                        .FirstOrDefaultAsync();

                    var targetEnrollment = await _context
                        .StudentEnrollment
                        .Where(_ => _.StudentEnrollmentId == newSubmission.StudentEnrollmentId)
                        .FirstOrDefaultAsync();

                    if (targetEnrollment == null) 
                        return Unauthorized();

                    if (targetDocument == null)
                    {
                        ModelState.AddModelError("Errors", "That document could not be found");
                        return BadRequest(ModelState);
                    }
                }
                return Unauthorized();
            }
            return Ok();
        }

        [HttpGet("{documentId}"), Authorize(Roles = "Instructor")]
        public async Task<ActionResult> Get(int documentId)
        {
            return Ok();
        }
    }
}