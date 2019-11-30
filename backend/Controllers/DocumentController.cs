using Microsoft.AspNetCore.Mvc;
using backend.Data.Contexts;
using System.Threading.Tasks;
using backend.Data.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using backend.Infrastructure.ClaimsManager;
using System.Collections.Generic;

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

        [HttpDelete("{documentId}"), Authorize(Roles = "Instructor")]
        public async Task<ActionResult> Delete(int documentId)
        {
            var claimsManager = new ClaimsManager(HttpContext.User);
            
            var targetDoc = await _context
                .Registrations
                .Select(_ => _.Documents
                    .Where(doc => doc.DocumentId == documentId)
                    .FirstOrDefault())
                .FirstOrDefaultAsync();

                if (targetDoc != null)
                {
                    var docRegistration = await _context
                    .Registrations
                    .Where(_ => _.RegistrationId == targetDoc.RegistrationId)
                    .FirstOrDefaultAsync();

                    if (docRegistration.InstructorId == claimsManager.GetUserIdClaim())
                    {
                        _context.Remove(targetDoc);
                        await _context.SaveChangesAsync();
                        return NoContent();
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
                ModelState.AddModelError("Errors", "The document could not be found");
                return BadRequest(ModelState);
        }

        [HttpGet("{registrationId}"), Authorize(Roles = "Instructor, Student")]
        public async Task<ActionResult> Get(int registrationId)
        {
            var claimsManager = new ClaimsManager(HttpContext.User);

            var targetRegistration = await _context
                .Registrations
                .Where(_ => _.RegistrationId == registrationId)
                .FirstOrDefaultAsync();

            if (targetRegistration != null)
            {
                var docs = await _context
                    .Registrations
                    .Where(_ => _.RegistrationId == targetRegistration.RegistrationId)
                    .Select(_ => _.Documents
                        .Select(doc => new Document
                        {
                            DocumentId = doc.DocumentId,
                            RegistrationId = doc.RegistrationId,
                            ResourceLink = doc.ResourceLink
                        }).ToList())
                        .FirstOrDefaultAsync();

                switch (claimsManager.GetRoleClaim())
                {
                    case "Student":

                        var associatedEnrollment = await _context
                            .StudentEnrollment
                            .Where(_ => _.RegistrationId == targetRegistration.RegistrationId
                                && _.StudentId == claimsManager.GetUserIdClaim())
                            .FirstOrDefaultAsync();

                        if (associatedEnrollment != null)
                        {
                            return Ok(docs);
                        }
                        ModelState.AddModelError("Errors", "You are not enrolled in this course");
                        return BadRequest(ModelState);

                    case "Instructor":
                        if (targetRegistration.InstructorId == claimsManager.GetUserIdClaim())
                        {
                            return Ok(docs);
                        }
                        return Unauthorized();
                }
                return Unauthorized();
            }
            else
            {
                ModelState.AddModelError("Errors", "That registration could not be found");
                return BadRequest(ModelState);
            }
        }
    }
}