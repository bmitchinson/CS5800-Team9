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
using backend.Infrastructure.PasswordSecurity;
using backend.Infrastructure.ClaimsManager;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InstructorController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Instructor>>> Get()
        {
            return await _context
                .Instructors
                .GetInstructors()
                .ToListAsync();
        }

        [HttpGet("{id}"), Authorize(Roles = "Student, Instructor, Admin")]
        public async Task<ActionResult<Instructor>> Get(int id)
        {

            var claimsManager = new ClaimsManager(HttpContext.User);

            var instructor = await _context
                .Instructors
                .GetInstructors()
                .Where(_ => _.InstructorId == id)
                .FirstOrDefaultAsync();

            switch (claimsManager.GetRoleClaim())
            {
                case "Instructor":
                    if (instructor.InstructorId == claimsManager.GetUserIdClaim())
                    {
                        return Ok(instructor);
                    }
                    else
                    {
                        return Unauthorized();
                    }
                case "Student":
                    return Ok(instructor);
                case "Admin":
                    return Ok(instructor);
            }
            return Unauthorized();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                if (!PasswordSecurity.CheckPasswordPolicy(instructor.Password))
                {
                    ModelState.AddModelError("Errors", "PASSWORD INVALID");
                    return BadRequest(ModelState);
                }
                if (_context.EmailIsTaken(instructor.Email))
                {
                    ModelState.AddModelError("Errors","Email has already been taken");
                    return BadRequest(ModelState);
                }
                instructor.Password = PasswordSecurity
                    .HashPassword(instructor.Password);
                await _context.AddAsync(instructor);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Get), new { id = instructor.InstructorId }, instructor);
            }
            return BadRequest();
        }

        [HttpDelete, Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var target = await
                _context
                .Instructors
                .Where(_ => _.InstructorId == id)
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