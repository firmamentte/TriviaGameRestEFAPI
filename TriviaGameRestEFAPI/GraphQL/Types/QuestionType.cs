using GraphQL.Types;
using TriviaGameRestEFAPI.BLL.DataContract;

namespace TriviaGameRestEFAPI.GraphQL.Types
{
    public class QuestionType : ObjectGraphType<QuestionResp>
    {
        public QuestionType()
        {
            Name = "Question";
            Field<NonNullGraphType<IdGraphType>>("questionId");
            Field<NonNullGraphType<IntGraphType>>("questionNumber");
            Field<NonNullGraphType<StringGraphType>>("questionDescription");
            Field<NonNullGraphType<IntGraphType>>("questionDuration");
            Field<NonNullGraphType<ListGraphType<ChoiceType>>>("choices");
        }
    }
}
