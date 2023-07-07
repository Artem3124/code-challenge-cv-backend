using CodeProblemAssistant.Contract.Models;
using CodeProblemAssistant.Data.Enums;
using CodeRunManager.Contract.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Gateway.Contact.Models
{
#nullable disable
    public class CodeProblem
    {
        public Guid UUID { get; set; }

        public string Name { get; set; }
        [JsonConverter(typeof(StringEnumConverter), typeof(CamelCaseNamingStrategy))]
        public CodeProblemComplexity ComplexityTypeId { get; set; }

        public string Description { get; set; }

        public List<string> Constraints { get; set; }

        public int UpVotesCount { get; set; }

        public int DownVotesCount { get; set; }

        public List<Example> Examples { get; set; }

        public List<string> Tags { get; set; }

        public CodeProblemState State { get; set; }
    }
#nullable enable
}
