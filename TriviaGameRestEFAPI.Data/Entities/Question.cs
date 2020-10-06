using System;
using System.Collections.Generic;

namespace TriviaGameRestEFAPI.Data.Entities
{
    public partial class Question
    {
        public Question()
        {
            Answer = new HashSet<Answer>();
            Choice = new HashSet<Choice>();
        }

        public Guid QuestionId { get; set; }
        public Guid GenreId { get; set; }
        public int QuestionNumber { get; set; }
        public string QuestionDescription { get; set; }
        public int QuestionDuration { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual ICollection<Answer> Answer { get; set; }
        public virtual ICollection<Choice> Choice { get; set; }
    }
}
