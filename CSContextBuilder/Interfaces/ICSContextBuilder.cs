using CodeProblemAssistant.Contract.Models;
using Cs.TestContextBuilder.Enums;
using CSTestContextBuilder.Core.Models;
using TestCases.Models;

namespace Cs.TestContextBuilder.Interfaces
{
    public interface ICSContextBuilder
    {
        CSCompilationContext Build(string sourceCode, CodeProblemMethodInfo methodInfo, List<TestCase>? testCases = default, List<CSContextBuildOptions>? contextBuildOptions = default);
    }
}