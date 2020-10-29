using System.Text.Json;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace TriviaGameRestEFAPI.GraphQL.Types
{
    [Route("graphql")]
    [ApiController]
    //[GraphQLAuthenticateCompanyAccount]
    public class GraphQL : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public GraphQL(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        //[HttpPost]
        public async Task<IActionResult> Post([FromBody] JsonElement query)
        {
            var _query = JsonConvert.DeserializeObject<GraphQLQuery>(query.ToString());

            var _result = await new DocumentExecuter().ExecuteAsync(options =>
            {
                options.Schema = new Schema
                {
                    Query = new Query(),
                    Mutation = new Mutation()
                };
                options.Query = _query.Query;
                options.OperationName = _query.OperationName;
                options.Inputs = _query.Inputs;
                options.UserContext = new GraphQLUserContext()
                {
                    //{"AccessToken",ControllerHelper.GetAccessToken(Request) },
                    //{"Username",ControllerHelper.GetUsername(Request) },
                    //{"UserPassword",ControllerHelper.GetUserPassword(Request) },
                    //{"CurrentPassword",ControllerHelper.GetCurrentPassword(Request) },
                    //{"NewPassword",ControllerHelper.GetNewPassword(Request) },
                    //{"ActivateAccountId",ControllerHelper.GetActivateAccountId(Request) },
                    {"IWebHostEnvironment",_webHostEnvironment }
                };
                //options.ComplexityConfiguration = new ComplexityConfiguration { MaxDepth = 15 };
            });

            if (_result.Errors?.Count > 0)
            {
                return BadRequest(new ApiErrorResp(_result.Errors));
            }

            return Ok(_result);
        }
    }
}
