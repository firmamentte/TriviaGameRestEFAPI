using GraphQL.Types;
using TriviaGameRestEFAPI.BLL.DataContract;

namespace TriviaGameRestEFAPI.GraphQL.Types
{
    public class GameResultType : ObjectGraphType<GameResultResp>
    {
        public GameResultType()
        {
            Name = "GameResult";
            Field<NonNullGraphType<IdGraphType>>("gameId");
            Field<NonNullGraphType<StringGraphType>>("genreName");
            Field<NonNullGraphType<DateTimeGraphType>>("creationDate");
            Field<NonNullGraphType<IntGraphType>>("totalScore");
            Field<NonNullGraphType<ListGraphType<AnswerType>>>("answers");
        }
    }
}
