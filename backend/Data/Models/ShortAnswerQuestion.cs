namespace backend.Data.Models
{
    public class ShortAnswerQuestion
    {
        public int ShortAnswerQuestionId { get; set; }

        public int QuestionId { get; set; }

        public string QuestionText { get; set; }

        public Question Question { get; set; }
    }
}