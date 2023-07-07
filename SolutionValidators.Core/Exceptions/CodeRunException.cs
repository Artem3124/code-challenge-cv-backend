using Shared.Core.Enums;

namespace SolutionValidators.Core.Exceptions
{
    public class CodeRunException : Exception
    {
        public CodeRunOutcome Reason { get; set; }

        public Dictionary<string, string> Metadata { get; set; }

        public CodeRunException(CodeRunOutcome reason, Dictionary<string, string> metadata)
        {
            Reason = reason;
            Metadata = metadata;
        }
    }
}
