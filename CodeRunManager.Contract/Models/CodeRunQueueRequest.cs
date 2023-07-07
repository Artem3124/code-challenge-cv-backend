using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Shared.Core.Enums;

namespace CodeRunManager.Contract.Models
{
    public class CodeRunQueueRequest
    {
        public Guid UserReferenceUUID { get; set; }

        public Guid CodeProblemReferenceUUID { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]

        public CodeLanguage CodeLanguageId { get; set; }

        public string SourceCode { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public RunType RunTypeId { get; set; }

        public CodeRunQueueRequest(
            Guid userReferenceUUID,
            Guid codeProblemReferenceUUID,
            CodeLanguage codeLanguageId,
            string sourceCode,
            RunType runTypeId)
        {
            UserReferenceUUID = userReferenceUUID;
            CodeProblemReferenceUUID = codeProblemReferenceUUID;
            CodeLanguageId = codeLanguageId;
            SourceCode = sourceCode;
            RunTypeId = runTypeId;
        }
    }
}
