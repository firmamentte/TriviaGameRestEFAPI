
using System;

namespace TriviaGameRestEFAPI.BLL.DataContract
{
    public class ChoiceResp
    {
        public Guid ChoiceId { get; set; }
        public string ChoiceName { get; set; }
        public bool IsCorrect { get; set; }
    }
}
