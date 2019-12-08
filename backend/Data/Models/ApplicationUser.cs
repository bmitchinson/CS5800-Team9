using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace backend.Data.Models
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
    }
}