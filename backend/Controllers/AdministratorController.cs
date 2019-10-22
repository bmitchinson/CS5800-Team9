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
    public class AdministratorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdministratorController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Administrator>>> Get()
        {
            return await _context
                .Administrators
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Administrator>> Get(int id)
        {
            return await _context
                .Administrators
                .Where(_ => _.AdministratorId == id)
                .FirstOrDefaultAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Administrator administrator)
        {
            if (ModelState.IsValid)
            {
                await _context.AddAsync(administrator);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Get), new { id = administrator.AdministratorId }, administrator);
            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var target = await
                _context
                .Administrators
                .Where(_ => _.AdministratorId == id)
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