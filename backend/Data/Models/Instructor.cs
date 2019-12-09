using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace backend.Data.Models
{
    public class Instructor
    {
        public int InstructorId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
                
        [Required]
        public DateTime? BirthDate { get; set; }

        public string Password { get; set; }
        public bool EmailConfirmed { get; set; }
        public ICollection<Registration> Registrations { get; set; }
    }
}