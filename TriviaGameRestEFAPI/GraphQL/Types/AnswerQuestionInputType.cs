using GraphQL.Types;
using TriviaGameRestEFAPI.BLL.DataContract;

namespace TriviaGameRestEFAPI.GraphQL.Types
{
    public class AnswerQuestionInputType : InputObjectGraphType<AnswerQuestionReq>
    {
        public AnswerQuestionInputType()
        {
            Name = "AnswerQuestionInput";
            Field<NonNullGraphType<IdGraphType>>("gameId");
            Field<NonNullGraphType<IdGraphType>>("questionId");
            Field<IdGraphType>("choiceId");
            Field<NonNullGraphType<IntGraphType>>("answerDuration");
        }
    }
}
