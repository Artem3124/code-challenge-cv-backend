using Shared.Core.Enums;

namespace CodeRunManager.Contract.Models
{
    public class CodeRunCompleteRequest
    {
        public CodeRunOutcome Outcome { get; set; }

        public Dictionary<string, string> Metadata { get; set; }

        public CodeRunCompleteRequest(CodeRunOutcome outcome, Dictionary<string, string>? metadata)
        {
            Outcome = outcome;
            Metadata = metadata ?? new();
        }
    }
}
