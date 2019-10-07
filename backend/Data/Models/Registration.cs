namespace backend.Data.Models
{
    public class Registration
    {
        // StudentId, InstructorId, and Course Id are the foreign key
        // relationships, together they make the primary key
        public int StudentId { get; set; }

        public int InstructorId { get; set; }

        public int CourseId { get; set; }

        public Instructor Instructor { get; set ;}

        public Student Student { get; set; }

        public Course Course { get; set; }
    }
}