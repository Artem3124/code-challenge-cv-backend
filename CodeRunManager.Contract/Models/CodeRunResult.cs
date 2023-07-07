using Shared.Core.Enums;
using SolutionValidators.Core.Models;
using TestCases.Models;

namespace CodeRunManager.Contract.Models
{
    public class CodeRunResult
    {
        public Guid UUID { get; set; }

        public TestCaseResult? FailedTest { get; set; }

        public List<CompilationError>? CompilationErrors { get; set; }

        public string? ExceptionMessage { get; set; }

        public CodeRunOutcome CodeRunOutcomeId { get; set; }

        public float Duration { get; set; }

        public DateTime CompletedAtUtc { get; set; }
    }
}
