using System.Collections.Generic;
using System.Linq;
using GraphQL;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TriviaGameRestEFAPI.Data;

namespace TriviaGameRestEFAPI
{
    public class ApiErrorResp
    {
        public string Message { get; set; } = "Please correct the specified Errors and try again.";
        public IEnumerable<string> Errors { get; set; }
        public ApiErrorResp(string message)
        {
            Errors = new List<string>() { message };
        }

        public ApiErrorResp(ModelStateDictionary modelState)
        {
            Errors = modelState.Values.SelectMany(modelErrorCollection => modelErrorCollection.Errors).Select(modelError => modelError.ErrorMessage);
        }

        public ApiErrorResp(ExecutionErrors executionErrors)
        {
            List<string> _errorMessages = new List<string>();

            foreach (var executionError in executionErrors)
            {
                if (executionError?.InnerException?.Message != null)
                {
                    if (executionError.InnerException.Message != Utilities.EnumHelper.GetDescription(Utilities.EnumHelper.InputValidationError.InputValidationError))
                        _errorMessages.Add(executionError.InnerException.Message);
                }
                else
                {
                    _errorMessages.Add(executionError.Message);
                }
            }

            Errors = _errorMessages;
        }
    }
}
