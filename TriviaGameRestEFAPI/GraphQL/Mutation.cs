using System;
using GraphQL;
using GraphQL.Types;
using TriviaGameRestEFAPI.BLL;
using TriviaGameRestEFAPI.BLL.DataContract;
using TriviaGameRestEFAPI.GraphQL.Types;

namespace TriviaGameRestEFAPI.GraphQL
{
    public class Mutation : ObjectGraphType
    {
        public Mutation()
        {
            Field<GameMutation>("gameMutation", resolve: context => new { });
        }
    }

    public class GameMutation : ObjectGraphType
    {
        public GameMutation()
        {
            Name = "GameMutation";
            FieldAsync<NonNullGraphType<GameType>>("createGame",
            arguments: new QueryArguments(
            new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "genreName" }),
            resolve: async context =>
            {
                try
                {
                    var _userContext = context.UserContext as GraphQLUserContext;
                    //string _accessToken = _userContext["AccessToken"].ToString();

                    return await TriviaGameRestEFAPIBLL.GameHelper.CreateGame(context.GetArgument<string>("genreName"));
                }
                catch (Exception)
                {
                    throw;
                }
            });

            FieldAsync<NonNullGraphType<BooleanGraphType>>("answerQuestion",
            arguments: new QueryArguments(
            new QueryArgument<NonNullGraphType<AnswerQuestionInputType>> { Name = "answerQuestionInput" }),
            resolve: async context =>
            {
                try
                {
                    var _userContext = context.UserContext as GraphQLUserContext;
                    //string _accessToken = _userContext["AccessToken"].ToString();

                    return await TriviaGameRestEFAPIBLL.GameHelper.AnswerQuestion(context.GetArgument<AnswerQuestionReq>("answerQuestionInput"));
                }
                catch (Exception)
                {
                    throw;
                }
            });
        }
    }
}
