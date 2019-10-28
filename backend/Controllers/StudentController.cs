using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using backend.Data.Contexts;
using backend.Data.Models;
using Microsoft.EntityFrameworkCore;

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
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> Get(int id)
        {
            return await _context
                .Students
                .Where(_ => _.StudentId == id)
                .FirstOrDefaultAsync();
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