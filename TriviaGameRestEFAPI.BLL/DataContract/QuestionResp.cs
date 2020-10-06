using System;
using System.Collections.Generic;

namespace TriviaGameRestEFAPI.BLL.DataContract
{
    public class QuestionResp
    {
        public Guid QuestionId { get; set; }
        public int QuestionNumber { get; set; }
        public string QuestionDescription { get; set; }
        public int QuestionDuration { get; set; }
        public List<ChoiceResp> Choices { get; set; } = new List<ChoiceResp>();
    }
}
