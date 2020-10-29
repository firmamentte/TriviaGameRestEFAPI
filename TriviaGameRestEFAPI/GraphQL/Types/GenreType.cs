using GraphQL.Types;
using TriviaGameRestEFAPI.BLL.DataContract;

namespace TriviaGameRestEFAPI.GraphQL.Types
{
    public class GenreType : ObjectGraphType<GenreResp>
    {
        public GenreType()
        {
            Name = "Genre";
            Field<NonNullGraphType<StringGraphType>>("genreName", deprecationReason: "Field Deprecation Reason!");
        }
    }
}
