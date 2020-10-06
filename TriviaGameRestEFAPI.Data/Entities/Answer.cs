using System;
using System.Collections.Generic;

namespace TriviaGameRestEFAPI.Data.Entities
{
    public partial class Answer
    {
        public Guid AnswerId { get; set; }
        public Guid GameId { get; set; }
        public Guid QuestionId { get; set; }
        public Guid? ChoiceId { get; set; }
        public int AnswerDuration { get; set; }

        public virtual Choice Choice { get; set; }
        public virtual Game Game { get; set; }
        public virtual Question Question { get; set; }
    }
}
