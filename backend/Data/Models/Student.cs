using System.Collections.Generic;

namespace backend.Data.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Course> Courses { get; set; }
    }
}