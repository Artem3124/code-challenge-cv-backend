using CodeProblemAssistant.Contract.Models;
using SolutionValidator.Cpp.Models;
using TestCases.Models;

namespace SolutionValidator.Cpp.Interfaces
{
    public interface ICppTestContextBuilder
    {
        CppCompilationContext Build(string sourceCode, CodeProblemMethodInfo methodInfo, List<TestCase> testCases);
    }
}