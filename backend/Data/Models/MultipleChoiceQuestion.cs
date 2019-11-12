using System.Collections.Generic;

namespace backend.Data.Models
{
    public class MultipleChoiceQuestion
    {
        public int MultipleChoiceQuestionId { get; set; }

        public int QuestionId { get; set; }

        public string Answer { get; set; }

        public Assessment Assessment { get; set; }

        public ICollection<MultipleChoiceQuestionChoice> Choices { get; set; }
    }
}