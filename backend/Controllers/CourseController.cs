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
    public class CourseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CourseController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> Get()
        {
            return await _context
                .Courses
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> Get(int id)
        {
            return await _context
                .Courses
                .Where(_ => _.CourseId == id)
                .FirstOrDefaultAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Course course)
        {
            if (ModelState.IsValid)
            {
                await _context.AddAsync(course);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Get), new { id = course.CourseId }, course);
            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var target = await
                _context
                .Courses
                .Where(_ => _.CourseId == id)
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