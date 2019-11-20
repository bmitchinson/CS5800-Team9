using System.Collections.Generic;

namespace backend.Data.Models
{
    public class Assessment
    {
        public int AssessmentId { get; set; }

        public int RegistrationId { get; set; }

        public Registration Registration { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}