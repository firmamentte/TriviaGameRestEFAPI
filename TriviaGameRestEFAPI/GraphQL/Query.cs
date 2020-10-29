using System;
using GraphQL;
using GraphQL.Types;
using TriviaGameRestEFAPI.BLL;
using TriviaGameRestEFAPI.GraphQL.Types;

namespace TriviaGameRestEFAPI.GraphQL
{
    public class Query : ObjectGraphType
    {
        public Query()
        {
            Field<GameQuery>("gameQuery", resolve: context => new { });
        }
    }

    public class GameQuery : ObjectGraphType
    {
        public GameQuery()
        {
            Name = "GameQuery";
            FieldAsync<NonNullGraphType<ListGraphType<GenreType>>>("genres",
            resolve: async context =>
            {
                try
                {
                    var _userContext = context.UserContext as GraphQLUserContext;
                    //string _accessToken = _userContext["AccessToken"].ToString();

                    return await TriviaGameRestEFAPIBLL.GenreHelper.GetGenres();
                }
                catch (Exception)
                {
                    throw;
                }
            });

            FieldAsync<NonNullGraphType<GameResultType>>("viewGame",
            arguments: new QueryArguments(
            new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "gameId" }), deprecationReason: "Endpoint Deprecation Reason",
            resolve: async context =>
            {
                try
                {
                    var _userContext = context.UserContext as GraphQLUserContext;
                    //string _accessToken = _userContext["AccessToken"].ToString();

                    return await TriviaGameRestEFAPIBLL.GameHelper.ViewGame(context.GetArgument<Guid>("gameId"));
                }
                catch (Exception)
                {
                    throw;
                }
            });
        }
    }
}
