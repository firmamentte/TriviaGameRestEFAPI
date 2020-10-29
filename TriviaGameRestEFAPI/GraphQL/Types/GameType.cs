using GraphQL.Types;
using TriviaGameRestEFAPI.BLL.DataContract;

namespace TriviaGameRestEFAPI.GraphQL.Types
{
    public class GameType : ObjectGraphType<GameResp>
    {
        public GameType()
        {
            Name = "Game";
            Field<NonNullGraphType<IdGraphType>>("gameId");
            Field<NonNullGraphType<StringGraphType>>("genreName");
            Field<NonNullGraphType<DateTimeGraphType>>("creationDate");
            Field<NonNullGraphType<ListGraphType<QuestionType>>>("questions");
        }
    }
}
