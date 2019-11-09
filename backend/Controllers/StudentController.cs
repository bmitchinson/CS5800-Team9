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

        [HttpGet("{id}"), Authorize(Roles = "Student, Instructor")]
        public async Task<ActionResult<Student>> Get(int id)
        {
            // TODO this should be put into aquery object
            var claimsDict = new Dictionary<string, string>();

            HttpContext.User.Claims.ToList()
                .ForEach(_ => claimsDict.Add(_.Type, _.Value));

            // TODO move some of this stuff into its own files/methods so that controller actions
            // are not cluttered with garbage
            var userEmail = 
                claimsDict["Email"];
            var userRole =
                claimsDict["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
            var userId =
                int.Parse(claimsDict["UserId"]);

            var student = await _context
                .Students
                .GetStudents()
                .Where(_ => _.StudentId == id)
                .FirstOrDefaultAsync();

            switch (userRole)
            {
                case "Student":
                    if (student.StudentId == userId)
                    {
                        return student;
                    }
                    else
                    {
                        return Unauthorized();
                    }
                case "Instructor":
                    return student;
                case "Administrator":
                    return student;
            }
            return Unauthorized();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Student student)
        {
            if (ModelState.IsValid)
            {
                await _context.AddAsync(student);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Get), new { id = student.StudentId }, student);
            }
            return BadRequest();
        }

        [HttpDelete]
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