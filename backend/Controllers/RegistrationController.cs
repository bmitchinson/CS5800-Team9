using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using backend.Data.Models;
using backend.Data.Contexts;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        // [HttpPost, Authorize(Roles = "Instructor, Admin")]
        // public async Task<ActionResult> CreateRegistration()
        // {
        //     return Ok();
        // }
    }
}