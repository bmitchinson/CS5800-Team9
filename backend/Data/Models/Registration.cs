using System.Collections.Generic;

namespace backend.Data.Models
{
    public class Registration
    {
        // StudentId, InstructorId, and Course Id are the foreign key
        // relationships, together they make the primary key
        public int RegistrationId { get; set; }

        public int? CourseId { get; set; }

        public int? InstructorId { get; set; }

        public int EnrollmentLimit { get; set; }

        public ICollection<StudentEnrollment> StudentEnrollments { get; set; }
    }
}