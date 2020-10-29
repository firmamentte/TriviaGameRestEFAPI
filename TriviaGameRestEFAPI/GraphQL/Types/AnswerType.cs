using GraphQL.Types;
using TriviaGameRestEFAPI.BLL.DataContract;

namespace TriviaGameRestEFAPI.GraphQL.Types
{
    public class AnswerType : ObjectGraphType<AnswerResp>
    {
        public AnswerType()
        {
            Name = "Answer";
            Field<NonNullGraphType<BooleanGraphType>>("isQuestionAnswered");
            Field<NonNullGraphType<BooleanGraphType>>("isAnswerCorrect");
            Field<NonNullGraphType<StringGraphType>>("questionDescription");
            Field<NonNullGraphType<IntGraphType>>("questionNumber");
            Field<NonNullGraphType<IntGraphType>>("questionDuration");
            Field<StringGraphType>("answerDescription");
            Field<NonNullGraphType<IntGraphType>>("answerDuration");
            Field<NonNullGraphType<IntGraphType>>("score");
        }
    }
}
