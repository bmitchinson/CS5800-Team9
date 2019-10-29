using backend.Data.Models;

namespace backend.ViewModels
{
    public class GetRegistrationViewModel
    {
        int RegistrationId { get; set; }

        int? StudentId { get; set; }

        int? CourseId { get; set; }

        int? InstructorId { get; set; }

        Student Student { get; set; }

        Course Course { get; set; }

        Instructor Instructor { get; set; }
    }
}