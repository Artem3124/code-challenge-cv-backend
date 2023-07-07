using CodeProblemAssistant.Contract.Models;
using CodeProblemAssistant.Data.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Gateway.Contact.Models
{
#nullable disable
    public class CreateCodeProblemRequest
    {
        public string Name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public CodeProblemComplexity ComplexityTypeId { get; set; }

        public string Explanation { get; set; }

        public string Description { get; set; }

        public List<string> Constraints { get; set; }

        public List<Example> Examples { get; set; }

        public List<string> Tags { get; set; }

        public List<string> ParameterNames { get; set; }

        public List<InternalType> ParameterTypes { get; set; }

        public InternalType ReturnType { get; set; }

        public Guid TestCaseSetUUID { get; set; }
    }
#nullable enable
}
