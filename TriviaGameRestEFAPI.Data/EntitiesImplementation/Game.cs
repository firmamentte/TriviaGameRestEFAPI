
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TriviaGameRestEFAPI.Data.Entities
{
    public partial class Game
    {
        public string GenreName
        {
            get
            {
                return Genre.GenreName;
            }
        }

        [NotMapped]
        public virtual List<Question> Questions
        {
            get
            {
                List<Question> _questions = new List<Question>();

                foreach (var question in Genre.Question)
                {
                    _questions.Add(question);
                }

                return _questions;
            }
        }

        public int TotalScore
        {
            get
            {
                int _totalScore = 0;

                foreach (var answer in Answer)
                {
                    _totalScore += answer.Score;
                }

                return _totalScore;
            }
        }
    }
}
