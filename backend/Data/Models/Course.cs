using System.Collections.Generic;
using System;
using backend.Data.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace backend.Data.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        [Required]
        public string CourseName { get; set; }

        [Required]
        public int CreditHours { get; set; }

        [Required]
        public string Section { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        // this tag converts the enum value into a string to the frontend
        [JsonConverter(typeof(StringEnumConverter))]
        public TopicLevel Level { get; set; }

        public ICollection<Registration> Registrations { get; set; }

        [BindNever]
        public bool SoftDeleted { get; set; }
    }
}