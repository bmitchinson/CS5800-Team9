using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace backend.Data.Models
{
    public class Registration
    {
        // StudentId, InstructorId, and Course Id are the foreign key
        // relationships, together they make the primary key
        public int RegistrationId { get; set; }

        public int? CourseId { get; set; }

        public int? InstructorId { get; set; }

        [Required]
        public int EnrollmentLimit { get; set; }

        public ICollection<StudentEnrollment> StudentEnrollments { get; set; }

        [Required]
        public Course Course { get; set; }

        [Required]
        public Instructor Instructor { get; set; }

        public ICollection<Prerequisite> Prerequisites { get; set; }

        public ICollection<Document> Documents { get; set; }

        public ICollection<Assessment> Assessments { get; set; }
    }
}