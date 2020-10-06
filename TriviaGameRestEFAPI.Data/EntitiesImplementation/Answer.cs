
namespace TriviaGameRestEFAPI.Data.Entities
{
    public partial class Answer
    {
        public bool IsQuestionAnswered
        {
            get
            {
                return Choice != null;
            }
        }

        public string QuestionDescription
        {
            get
            {
                return Question.QuestionDescription;
            }
        }

        public int QuestionNumber
        {
            get
            {
                return Question.QuestionNumber;
            }
        }

        public int QuestionDuration
        {
            get
            {
                return Question.QuestionDuration;
            }
        }

        public bool IsAnswerCorrect
        {
            get
            {
                if (Choice != null)
                    return Choice.IsCorrect;
                else
                    return false;
            }
        }

        public string AnswerDescription
        {
            get
            {
                if (Choice != null)
                    return Choice.ChoiceName;
                else
                    return null;
            }
        }

        public int Score
        {
            get
            {
                if (IsAnswerCorrect)
                    return QuestionDuration - AnswerDuration;
                else
                    return 0;
            }
        }
    }
}
