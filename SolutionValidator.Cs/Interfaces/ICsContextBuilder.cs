using CodeProblemAssistant.Contract.Models;
using SolutionValidator.Cs.Enums;
using SolutionValidator.Cs.Models;
using TestCases.Models;

namespace SolutionValidator.Cs.Interfaces
{
    public interface ICsContextBuilder
    {
        CsCompilationContext Build(string sourceCode, CodeProblemMethodInfo methodInfo, List<TestCase>? testCases = default, List<CSContextBuildOptions>? contextBuildOptions = default);
    }
}