using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using backend.Data.Models;
using backend.Data.Contexts;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using backend.Data.QueryObjects;
using System.Collections.Generic;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RegistrationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet, Authorize(Roles = "Student, Admin, Instructor")]
        public async Task<ActionResult> GetRegistrations()
        {
            var registrations = await _context
                .Registrations
                .GetRegistrations()
                .ToListAsync();

            return Ok(registrations);
        }

        [HttpGet("{id}"), Authorize(Roles = "Student, Admin, Instructor")]
        public async Task<ActionResult> GetRegistration(int id)
        {
            var registrations = await _context
                .Registrations
                .GetRegistrations()
                .Where(_ => _.RegistrationId == id)
                .ToListAsync();

            return Ok(registrations);
        }

        [HttpPost, Authorize(Roles = "Instructor, Admin")]
        public async Task<ActionResult> CreateRegistration([FromBody]Registration registration)
            {
                if (ModelState.IsValid)
                {
                    var course = await _context
                        .Courses
                        .Where(_ => _.CourseId == registration.Course.CourseId)
                        .FirstOrDefaultAsync();

                    var instructor = await _context
                        .Instructors
                        .Where(_ => _.InstructorId == registration.Instructor.InstructorId)
                        .FirstOrDefaultAsync();
                    
                    if (course != null && instructor != null)
                    {
                        var newRegistration = new Registration
                        {
                            Course = course,
                            Instructor = instructor,
                            // Prerequisites = registration.Prerequisites
                        };

                        await _context.AddAsync(newRegistration);
                        await _context.SaveChangesAsync();
                        return CreatedAtAction(
                            nameof(GetRegistration), 
                            new { id = newRegistration.RegistrationId }, 
                            newRegistration);
                    }
                    else
                    {
                        ModelState.AddModelError("Errors", "Instructor or course does not exist");
                    }
                }

                return BadRequest(ModelState);
            }
        
        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteRegistration(int id)
        {
            var target = await _context
                .Registrations
                .Where(_ => _.RegistrationId == id)
                .FirstOrDefaultAsync();

            if (target != null)
            {
                _context.Remove(target);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}