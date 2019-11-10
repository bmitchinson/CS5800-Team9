using System.Collections.Generic;
using System;
using backend.Data.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace backend.Data.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        public string CourseName { get; set; }

        public int CreditHours { get; set; }

        public string Section { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        // this tag converts the enum value into a string to the frontend
        [JsonConverter(typeof(StringEnumConverter))]
        public TopicLevel Level { get; set; }

        public ICollection<Registration> Registrations { get; set; }
    }
}