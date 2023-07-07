using CodeProblemAssistant.Contract.Models;
using CodeRunManager.Contract.Models;
using Mongo.Data.Models;
using Shared.Core.Compilers;
using TestCases.Models;

namespace SolutionValidators.Core.ContextModels
{
    public abstract class RunContextBase : IRunContext
    {
#nullable disable
        public CodeRunQueueMessage CodeRun { get; set; }

        public Challenge Challenge { get; set; }

        public CodeProblem CodeProblem { get; set; }

        public List<TestCase> TestCases { get; set; } = new();

        public List<TestCaseResult> TestCaseResults { get; set; } = new();

        public List<CompilationDiagnostic> Diagnostics { get; set; } = new();
#nullable enable
        public Guid InternalAssemblyUUID { get; set; }

        public RunContextBase(IRunContext context)
        {
            CodeRun = context.CodeRun;
            CodeProblem = context.CodeProblem;
            TestCases = context.TestCases;
            TestCaseResults = context.TestCaseResults;
            InternalAssemblyUUID = context.InternalAssemblyUUID;
            Diagnostics = context.Diagnostics;
        }

        public RunContextBase(CodeRunQueueMessage codeRun)
        {
            CodeRun = codeRun;
        }

        public abstract void Validate();
    }
}
