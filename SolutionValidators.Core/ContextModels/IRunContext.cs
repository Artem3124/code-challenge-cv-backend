using CodeProblemAssistant.Contract.Models;
using Mongo.Data.Models;
using Shared.Core.Compilers;
using TestCases.Models;

namespace SolutionValidators.Core.ContextModels
{
    public interface IRunContext
    {
        public CodeRunQueueMessage CodeRun { get; set; }

        public CodeProblem CodeProblem { get; set; }

        public Challenge Challenge { get; set; }

        public List<TestCase> TestCases { get; set; }

        public List<TestCaseResult> TestCaseResults { get; set; }

        public List<CompilationDiagnostic> Diagnostics { get; set; }

        public Guid InternalAssemblyUUID { get; set; }

        void Validate();
    }
}
