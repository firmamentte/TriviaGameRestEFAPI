using System;
using System.Collections.Generic;

namespace TriviaGameRestEFAPI.Data.Entities
{
    public partial class Choice
    {
        public Choice()
        {
            Answer = new HashSet<Answer>();
        }

        public Guid ChoiceId { get; set; }
        public Guid QuestionId { get; set; }
        public string ChoiceName { get; set; }
        public bool IsCorrect { get; set; }

        public virtual Question Question { get; set; }
        public virtual ICollection<Answer> Answer { get; set; }
    }
}
