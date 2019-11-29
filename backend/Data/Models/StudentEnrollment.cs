namespace backend.Data.Models
{
    public class StudentEnrollment 
    {
        public int StudentEnrollmentId { get; set; }
        
        public int StudentId { get; set; }

        public int RegistrationId { get; set; }

        public Registration Registration { get; set; }

        public Student Student { get; set; }
    }
}