using System.Collections.Generic;
using System.Linq;
using GraphQL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TriviaGameRestEFAPI.GraphQL
{
    public class GraphQLQuery
    {
        public string OperationName { get; set; }
        public string NamedQuery { get; set; }
        public string Query { get; set; }
        public Dictionary<string, object> Variables { get; set; }
        public Inputs Inputs
        {
            get
            {
                if (Variables != null && Variables.Any())
                {
                    Dictionary<string, object> _variables = new Dictionary<string, object>();

                    foreach (var key in Variables.Keys)
                    {
                        var _value = Variables[key];

                        if (_value is JObject)
                        {
                            // fix the nesting ( {{ }} )
                            var _serialized = JsonConvert.SerializeObject(_value);
                            var _deserialized = JsonConvert.DeserializeObject<Dictionary<string, object>>(_serialized);
                            _variables.Add(key, _deserialized);
                        }
                        else
                        {
                            _variables.Add(key, _value);
                        }
                    }

                    return new Inputs(_variables);
                }
                else
                {
                    return (Variables != null) ? new Inputs(Variables) : new Inputs(new Dictionary<string, object>());
                }
            }
        }
    }
}
