
using System;

namespace TriviaGameRestEFAPI.BLL.DataContract
{
    public class AnswerQuestionReq
    {
        public Guid GameId { get; set; }
        public Guid QuestionId { get; set; }
        public Guid? ChoiceId { get; set; }
        public int AnswerDuration { get; set; }
    }
}
