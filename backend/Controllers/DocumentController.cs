using Microsoft.AspNetCore.Mvc;
using backend.Data.Contexts;
using System.Threading.Tasks;
using backend.Data.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using backend.Infrastructure.ClaimsManager;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DocumentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost, Authorize(Roles = "Instructor")]
        public async Task<ActionResult> CreateDocument(Document newDocument)
        {
            var claimsManager = new ClaimsManager(HttpContext.User);

            if (ModelState.IsValid)
            {
                if (claimsManager.GetRoleClaim() == "Instructor")
                {   
                    var targetRegistration = await _context
                        .Registrations
                        .Where(_ => _.RegistrationId == newDocument.RegistrationId
                            && _.InstructorId == claimsManager.GetUserIdClaim())
                        .FirstOrDefaultAsync();

                    if (targetRegistration != null)
                    {
                        await _context.AddAsync(newDocument);
                        await _context.SaveChangesAsync();
                        return Ok();
                    }
                    ModelState.AddModelError("Errors", "Registration was not found");
                    return BadRequest(ModelState);
                }
                return Unauthorized();
            }
            return BadRequest(ModelState);
        }
    }
}