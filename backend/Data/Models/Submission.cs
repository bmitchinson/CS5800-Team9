namespace backend.Data.Models
{
    public class Submission
    {
        public int SubmissionId { get; set; }

        public int DocumentId { get; set; }

        public int StudentEnrollmentId { get; set; }

        public string Grade { get; set; }

        public string ResourceLink { get; set; }

        public Document Document { get; set; }

        public StudentEnrollment StudentEnrollment { get; set; }
    }
}