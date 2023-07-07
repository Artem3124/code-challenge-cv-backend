using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Shared.Core.Enums;

namespace Gateway.Contact.Models
{
    public class CodeSubmitRequest
    {
        [JsonConverter(typeof(StringEnumConverter), typeof(CamelCaseNamingStrategy))]
        public CodeLanguage CodeLanguage { get; set; }

        [JsonConverter(typeof(StringEnumConverter), typeof(CamelCaseNamingStrategy))]
        public RunType RunType { get; set; }

        public string SourceCode { get; set; } = string.Empty;

        public Guid CodeProblemUUID { get; set; }
    }
}
