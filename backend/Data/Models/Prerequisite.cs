namespace backend.Data.Models
{
    public class Prerequisite
    {
        public int CourseId  { get; set; }
        
        public int PrerequisiteId { get; set; }

        public bool  IsMandatory  { get; set; }

        public Course Course  { get; set; }
    }
}