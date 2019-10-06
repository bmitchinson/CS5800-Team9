using System.Collections.Generic;
using System;

namespace backend.Data.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        public ICollection<Registration> Registrations { get; set; }
    }
}