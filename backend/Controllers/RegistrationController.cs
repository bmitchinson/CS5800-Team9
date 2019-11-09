using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using backend.Data.Models;
using backend.Data.Contexts;
using System.Linq;
using System.Threading.Tasks;

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

        // public async Task<ActionResult> Get()
        // {
        //     var data =                 _context
        //         .Students
        //         .Include(_ => _.Registrations)
        //         .ThenInclude(r => r.Course)
        //         .ToList();

        //     return Ok(data);
        // }

        // public async Task<ActionResult> Get([FromQuery]int? studentId, [FromQuery]int? courseId, [FromQuery]int? instructorId)
        // {
        //     var registrations =  await _context
        //         .Students
        //         .QueryForRegistrations(studentId, instructorId, courseId)
        //         .FirstOrDefaultAsync();

        //     return Ok(registrations);
        // }
    }
}