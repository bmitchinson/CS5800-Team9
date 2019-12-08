using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using backend.Models;
using backend.Data.Contexts;
using backend.Data.Models;
using Microsoft.EntityFrameworkCore;
using backend.Data.QueryObjects;
using backend.Infrastructure.EmailManager;
using backend.Infrastructure.PasswordSecurity;
using backend.Infrastructure.ClaimsManager;
namespace backend.Controllers
{

    public class EmailController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailManager _emailManager;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;
        public EmailController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IEmailManager emailManager,
            ILoggerFactory loggerFactory,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailManager = emailManager;
            _logger = loggerFactory.CreateLogger<EmailController>();
            _context = context;
        }


        [HttpPost("api/[controller]/sendconfirmation")]
        [AllowAnonymous]
        public async  Task<IActionResult> SendConfirmation([FromBody]LoginModel signup)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    var user = new IdentityUser { UserName = signup.Email, Email = signup.Email };
                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=532713
                    // Send an email with this link
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action(nameof(Confirm), "Email", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                    var applicationUser = new ApplicationUser {UserId = user.Id, Email = signup.Email, Code = code};
                    await _context.AddAsync(applicationUser);
                    await _context.SaveChangesAsync();
                    _emailManager.Send(signup.Email, "Confirm your account", $"Please confirm your account by clicking this here: <a href='{callbackUrl}'>link</a>");
                    _logger.LogInformation(3, "User created a new account with password.");
                    return Ok();
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Errors", "There was a problem with the email client");
                    throw(e);
                }
            }
            return BadRequest();
        }

        [HttpGet("api/[controller]/confirm")]
        [AllowAnonymous]
        public async Task<IActionResult> Confirm(string userId, string code)
        {
            
            if (userId == null || code == null)
            {
                return BadRequest();
            }
        
            var user = await
                    _context
                    .ApplicationUsers
                    .Where(_ => _.UserId == userId)
                    .FirstOrDefaultAsync();

            if (user == null)
            {
                return BadRequest();
            }

            if (user.Code == code){
                var studentClaim = await
                _context
                .Students
                .Where(_ => _.Email == user.Email)
                .FirstOrDefaultAsync();
                
                if (studentClaim == null) 
                {
                    return BadRequest();
                } 
                studentClaim.EmailConfirmed = true;
                _context.Students.Update(studentClaim);
                await _context.SaveChangesAsync();
            }
            return Redirect("http://localhost:3000/confirm");
        }
        


    }
}