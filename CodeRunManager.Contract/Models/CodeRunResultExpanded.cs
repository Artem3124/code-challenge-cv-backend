using Shared.Core.Enums;
using SolutionValidators.Core.Models;
using TestCases.Models;

namespace CodeRunManager.Contract.Models
{
    public class CodeRunResultExpanded
    {
        public Guid UUID { get; set; }

        public Guid CodeProblemUUID { get; set; }

        public TestCaseResult? FailedTest { get; set; }

        public List<CompilationError>? CompilationErrors { get; set; }

        public string? ExceptionMessage { get; set; }

        public CodeRunOutcome CodeRunOutcomeId { get; set; }

        public string SourceCode { get; set; }

        public CodeLanguage CodeLanguage { get; set; }

        public RunType RunType { get; set; }

        public DateTime DateTimeUtc { get; set; }

        public float Duration { get; set; }

#nullable disable
        public CodeRunResultExpanded()
        {

        }
#nullable enable
        public CodeRunResultExpanded(CodeRunResult codeRunResult, Guid codeProblemUUID, string sourceCode, CodeLanguage codeLanguage, RunType runType)
        {
            UUID = codeRunResult.UUID;
            FailedTest = codeRunResult.FailedTest;
            CompilationErrors = codeRunResult.CompilationErrors;
            ExceptionMessage = codeRunResult.ExceptionMessage;
            CodeRunOutcomeId = codeRunResult.CodeRunOutcomeId;
            RunType = runType;
            Duration = codeRunResult.Duration;
            CodeProblemUUID = codeProblemUUID;
            SourceCode = sourceCode;
            CodeLanguage = codeLanguage;
            DateTimeUtc = codeRunResult.CompletedAtUtc;
        }
    }
}
