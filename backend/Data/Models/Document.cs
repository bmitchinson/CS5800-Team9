using System.Collections.Generic;

namespace backend.Data.Models
{
    public class Document
    {
        public int DocumentId { get; set; }

        public int RegistrationId { get; set; }

        public string ResourceLink { get; set; }

        public Registration Registration { get; set; }

        public ICollection<Submission> Submissions { get; set; }
    }
}