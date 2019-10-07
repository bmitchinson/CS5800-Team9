using System.Collections.Generic;

namespace backend.Data.Models
{
    public class Instructor
    {
        public int InstructorId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<Registration> Registrations { get; set; }
    }
}