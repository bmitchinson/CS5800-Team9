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
    public class CompletedCoursesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CompletedCoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{studentId}")]
        [Authorize (Roles = "Student")]
        public async Task<ActionResult> Get(int studentId)
        {
            var claimsManager = new ClaimsManager(HttpContext.User);

            if (studentId != claimsManager.GetUserIdClaim()) 
                { return Unauthorized(); }

            var completedCourses = await _context
                .StudentEnrollment
                .Where(_ => _.StudentId == claimsManager.GetUserIdClaim())
                .QueryCompletedCourses()
                .ToListAsync();

            return Ok(completedCourses);
        }
    }
}