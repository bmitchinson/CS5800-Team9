using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using backend.Data.Contexts;
using backend.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using backend.Data.QueryObjects;


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

        [HttpGet, Authorize(Roles = "Student, Admin, Instructor")]
        public ActionResult<IEnumerable<Course>> Get()
        {
            return Ok(_context
                .Courses
                .GetCourses()
                .FilterSoftDeleted()
                .ToList());
        }

        [HttpGet("{id}"), Authorize(Roles = "Admin, Instructor, Student")]
        public ActionResult<Course> Get(int id)
        {
            return _context
                .Courses
                .GetCourses()
                .Where(_ => _.CourseId == id)
                .FilterSoftDeleted()
                .FirstOrDefault();
        }

        [HttpPost, Authorize(Roles = "Admin")]
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

        [HttpDelete("{courseId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int courseId)
        {
            var target = await
                _context
                .Courses
                .Where(_ => _.CourseId == courseId)
                .FirstOrDefaultAsync();
            
            if (target != null)
            {
                target.SoftDeleted = true;
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