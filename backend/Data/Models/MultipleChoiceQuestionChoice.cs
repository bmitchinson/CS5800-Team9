namespace backend.Data.Models
{
    public class MultipleChoiceQuestionChoice
    {
        public int MultipleChoiceQuestionChoiceId { get; set; }

        public int MultipleChoiceQuestionId { get; set; }

        public string QuestionText { get; set; }

        // ex: a, b, c, 1, 2, 21, ....
        public string SelectionIdentifier { get; set; }

        public MultipleChoiceQuestion MultipleChoiceQuestion { get; set; }
    }
}