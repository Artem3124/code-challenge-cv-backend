using CodeProblemAssistant.Data.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace CodeProblemAssistant.Contract.Models
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

        public List<Example> Examples { get; set; }

        public int UpVotesCount { get; set; }

        public int DownVotesCount { get; set; }

        public List<Tag> Tags { get; set; }

        public CodeProblemMethodInfo MethodInfo { get; set; }
    }

    public struct CodeProblemMethodInfo
    {
        public string Name { get; set; }

        public List<CodeProblemParameterInfo> Parameters { get; set; }

        public InternalType ReturnType { get; set; }
    }

    public struct CodeProblemParameterInfo
    {
        public string Name { get; set; }

        public InternalType Type { get; set; }
    }


    public class Tag
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
#nullable enable
}
