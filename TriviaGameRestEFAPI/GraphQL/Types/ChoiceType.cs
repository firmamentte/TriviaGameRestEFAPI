using GraphQL.Types;
using TriviaGameRestEFAPI.BLL.DataContract;

namespace TriviaGameRestEFAPI.GraphQL.Types
{
    public class ChoiceType : ObjectGraphType<ChoiceResp>
    {
        public ChoiceType()
        {
            Name = "Choice";
            Field<NonNullGraphType<IdGraphType>>("choiceId");
            Field<NonNullGraphType<StringGraphType>>("choiceName");
            Field<NonNullGraphType<BooleanGraphType>>("isCorrect");
        }
    }
}
