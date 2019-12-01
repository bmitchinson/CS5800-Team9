using System.Collections.Generic;
using backend.Data.Enums;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace backend.Data.Models
{
    public class Document
    {
        public int DocumentId { get; set; }

        public int RegistrationId { get; set; }

        [Required]
        public string ResourceLink { get; set; }

        public Registration Registration { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [Required]
        public DocumentType DocType { get; set; }

        public ICollection<Submission> Submissions { get; set; }
    }
}