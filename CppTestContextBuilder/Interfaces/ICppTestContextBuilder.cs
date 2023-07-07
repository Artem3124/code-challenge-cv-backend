using CodeProblemAssistant.Contract.Models;
using CppTestContextBuilder.Core.Models;
using TestCases.Models;

namespace CppTestContextBuilder.Interfaces
{
    public interface ICppTestContextBuilder
    {
        CppCompilationContext Build(string sourceCode, CodeProblemMethodInfo methodInfo, List<TestCase> testCases);
    }
}