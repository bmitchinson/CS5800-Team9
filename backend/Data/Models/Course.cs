using System.Collections.Generic;
using System;

namespace backend.Data.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        public string CourseName { get; set; }

        public int CreditHours { get; set; }

        public string Section { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public ICollection<Registration> Registrations { get; set; }
        public ICollection<Course> PreRequisites { get; set; }
        public string level {get; set;}
    }
}