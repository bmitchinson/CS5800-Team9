namespace backend.Data.Models
{
    public class Question
    {
        public int QuestionId { get; set; }

        public int AssessmentId { get; set; }

        public Assessment Assessment { get; set; }

        // for a question there only exists one or another, there should be an
        // application level constraint that prevents a question from having both
        public MultipleChoiceQuestion MultipleChoiceQuestion { get; set; }

        public ShortAnswerQuestion ShortAnswerQuestion { get; set; }
    }
}