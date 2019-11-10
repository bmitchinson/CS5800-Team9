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
        public ActionResult GetRegistrations()
        {
            var registrations = _context
                .Instructors
                .GetRegistrations()
                .ToList();
            // var registrations = _context
            //     .Instructors
            //     .Select(_ => _.Registrations)
            //     .ToList();

            return Ok(registrations);
        }

        // [HttpPost, Authorize(Roles = "Instructor, Admin")]
        // public async Task<ActionResult> CreateRegistration()
        // {
        //     return Ok();
        // }
    }
}