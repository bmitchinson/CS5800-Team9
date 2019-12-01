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
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> Get()
        {
            return await _context
                .Students
                .GetStudents()
                .ToListAsync();
        }

        [HttpGet("{id}"), Authorize(Roles = "Student, Instructor, Admin")]
        public async Task<ActionResult<Student>> Get(int id)
        {

            var claimsManager = new ClaimsManager(HttpContext.User);

            var student = await _context
                .Students
                .GetStudents()
                .Where(_ => _.StudentId == id)
                .FirstOrDefaultAsync();

            switch (claimsManager.GetRoleClaim())
            {
                case "Student":
                    if (student.StudentId == claimsManager.GetUserIdClaim())
                    {
                        return Ok(student);
                    }
                    else
                    {
                        return Unauthorized();
                    }
                case "Instructor":
                    return Ok(student);
                case "Admin":
                    return Ok(student);
            }
            return Unauthorized();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Student student)
        {
            if (ModelState.IsValid)
            {
                if (!PasswordSecurity.CheckPasswordPolicy(student.Password))
                {
                    ModelState.AddModelError("ModelError", "PASSWORD INVALID");
                    return BadRequest(ModelState);
                }
                if (_context.EmailIsTaken(student.Email))
                {
                    ModelState.AddModelError("ModelError", "Email has already been taken");
                    return BadRequest(ModelState);
                }
                student.Password = PasswordSecurity
                    .HashPassword(student.Password);
                await _context.AddAsync(student);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Get), new { id = student.StudentId }, student);
            }
            return BadRequest();
        }

        [HttpDelete, Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var target = await
                _context
                .Students
                .Where(_ => _.StudentId == id)
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