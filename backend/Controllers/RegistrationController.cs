using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using backend.Data.Models;
using backend.Data.Contexts;
using System.Linq;
using System.Threading.Tasks;
using backend.Data.QueryObjects;

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

        public async Task<ActionResult> Get([FromQuery]int? studentId, [FromQuery]int? courseId, [FromQuery]int? instructorId)
        {
            var registrations =  await _context
                .Students
                .QueryForRegistrations(studentId, instructorId, courseId)
                .FirstOrDefaultAsync();
                
            return Ok(registrations);
        }
    }
}