
namespace TriviaGameRestEFAPI.BLL.DataContract
{
    public class AnswerResp
    {
        public bool IsQuestionAnswered { get; set; }
        public bool IsAnswerCorrect { get; set; }
        public string QuestionDescription { get; set; }
        public int QuestionNumber { get; set; }
        public int QuestionDuration { get; set; }
        public string AnswerDescription { get; set; }
        public int AnswerDuration { get; set; }
        public int Score { get; set; }
    }
}
