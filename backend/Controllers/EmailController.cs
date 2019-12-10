using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.Data.Contexts;
using backend.Data.Models;
using Microsoft.EntityFrameworkCore;
using backend.Infrastructure.EmailManager;
using backend.Infrastructure.PasswordSecurity;
namespace backend.Controllers

{
    public class EmailController : Controller
    {
        private readonly IEmailManager _emailManager;
        private readonly ApplicationDbContext _context;
        public EmailController(
            IEmailManager emailManager,
            ApplicationDbContext context)
        {
            _emailManager = emailManager;
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
                    var id = PasswordSecurity.HashEmail(signup.Email);
                    var code = PasswordSecurity.HashEmail(signup.Password);
                    var callbackUrl = Url.Action(nameof(Confirm), "Email", new { userId = id, code = code }, protocol: HttpContext.Request.Scheme);
                    var applicationUser = new ApplicationUser {UserId = id, Email = signup.Email, Code = code};
                    await _context.AddAsync(applicationUser);
                    await _context.SaveChangesAsync();
                    _emailManager.Send(signup.Email, "Confirm your account", $"Please confirm your account by clicking <a href='{callbackUrl}'>here</a>");
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
                return Redirect("http://localhost:3000/error");
            }
        
            var user = await
                    _context
                    .ApplicationUsers
                    .Where(_ => _.UserId == userId)
                    .FirstOrDefaultAsync();

            if (user == null)
            {
                return Redirect("http://localhost:3000/error");
            }

            if (user.Code == code){
                var studentClaim = await
                    _context
                    .Students
                    .Where(_ => _.Email == user.Email)
                    .FirstOrDefaultAsync();

                var instructorClaim = await
                    _context
                    .Instructors
                    .Where(_ => _.Email == user.Email)
                    .FirstOrDefaultAsync();
                
                if (studentClaim != null )
                {
                    studentClaim.EmailConfirmed = true;
                    _context.Students.Update(studentClaim);
                    await _context.SaveChangesAsync();
                }
                else if (instructorClaim != null)
                {
                    instructorClaim.EmailConfirmed = true;
                    _context.Instructors.Update(instructorClaim);
                    await _context.SaveChangesAsync();
                } 
                else 
                {
                    return Redirect("http://localhost:3000/error");
                }
            }

            return Redirect("http://localhost:3000/confirm");
        }
        

        [HttpPost("api/[controller]/forgotpassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody]ForgotPasswordModel forgotPassword)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var applicationuser = await
                    _context
                    .ApplicationUsers
                    .Where(_ => _.Email == forgotPassword.Email)
                    .FirstOrDefaultAsync();

                    var studentClaim = await
                        _context
                        .Students
                        .Where(_ => _.Email == forgotPassword.Email)
                        .FirstOrDefaultAsync();

                    var instructorClaim = await
                        _context
                        .Instructors
                        .Where(_ => _.Email == forgotPassword.Email)
                        .FirstOrDefaultAsync();

                    if (applicationuser == null)
                    {
                        // Don't reveal that the user does not exist 
                        return Ok();
                    }
                    if (!studentClaim.EmailConfirmed)
                    {
                        return Ok();
                    }
                    if (!studentClaim.EmailConfirmed)
                    {
                        return Ok();
                    }
        
                    var code = PasswordSecurity.HashPassword(forgotPassword.ResetPassword);
                    applicationuser.Code = code;
                    applicationuser.ResetPassword = PasswordSecurity
                        .HashPassword(forgotPassword.ResetPassword);
                    _context.ApplicationUsers.Update(applicationuser);
                    await _context.SaveChangesAsync();
                    var callbackUrl = Url.Action(nameof(ResetPassword), "Email", new { userId = applicationuser.UserId, code = code }, protocol: HttpContext.Request.Scheme);
                    var callbackUrl2 = Url.Action(nameof(CancelReset), "Email", new { userId = applicationuser.UserId, code = code }, protocol: HttpContext.Request.Scheme);

                    _emailManager.Send(forgotPassword.Email, "Reset Password", $"Please reset your password by clicking <a href='{callbackUrl}'>here</a><br>Not you? Click <a href='{callbackUrl2}'>here</a>");
                    
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Errors", "There was a problem with the email client");
                    throw(e);
                }
                
                
            }
            return Ok();
        }

        
        [HttpGet("api/[controller]/resetpassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return Redirect("http://localhost:3000/error");
            }
        
            var user = await
                    _context
                    .ApplicationUsers
                    .Where(_ => _.UserId == userId)
                    .FirstOrDefaultAsync();

            if (user == null)
            {
                return Redirect("http://localhost:3000/error");
            }

            if (user.Code == code){
                var studentClaim = await
                    _context
                    .Students
                    .Where(_ => _.Email == user.Email)
                    .FirstOrDefaultAsync();

                var instructorClaim = await
                    _context
                    .Instructors
                    .Where(_ => _.Email == user.Email)
                    .FirstOrDefaultAsync();
                
                if (studentClaim != null && studentClaim.EmailConfirmed)
                {
                    studentClaim.Password = user.ResetPassword;
                    studentClaim.EmailConfirmed = true;
                    _context.Students.Update(studentClaim);
                    await _context.SaveChangesAsync();
                }
                else if (instructorClaim != null && instructorClaim.EmailConfirmed)
                {
                    studentClaim.Password = user.ResetPassword;
                    _context.Students.Update(studentClaim);
                    await _context.SaveChangesAsync();
                    
                } 
                else 
                {
                    return Redirect("http://localhost:3000/error");
                }
            
            }
            return Redirect("http://localhost:3000/resetpassword");
        }

        [HttpGet("api/[controller]/cancelreset")]
        [AllowAnonymous]
        public async Task<IActionResult> CancelReset(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return Redirect("http://localhost:3000/error");
            }

            var user = await
                    _context
                    .ApplicationUsers
                    .Where(_ => _.UserId == userId)
                    .FirstOrDefaultAsync();

            if (user == null)
            {
                return Redirect("http://localhost:3000/error");
            }

            if (user.Code == code){
                var studentClaim = await
                    _context
                    .Students
                    .Where(_ => _.Email == user.Email)
                    .FirstOrDefaultAsync();

                var instructorClaim = await
                    _context
                    .Instructors
                    .Where(_ => _.Email == user.Email)
                    .FirstOrDefaultAsync();

                //Reset the passwords to their original value so the reset link won't reset anything
                if (studentClaim != null )
                {
                    user.ResetPassword = studentClaim.Password;
                    _context.ApplicationUsers.Update(user);
                    await _context.SaveChangesAsync();
                }
                else if (instructorClaim != null )
                {
                    user.ResetPassword = studentClaim.Password ;
                    _context.ApplicationUsers.Update(user);
                    await _context.SaveChangesAsync();
                    
                } 
                else 
                {
                    return Redirect("http://localhost:3000/error");
                }
            
            }
            return Redirect("http://localhost:3000/cancelreset");
        }

    }
}