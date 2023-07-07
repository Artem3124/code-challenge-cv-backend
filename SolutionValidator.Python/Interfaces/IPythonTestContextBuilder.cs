using CodeProblemAssistant.Contract.Models;
using SolutionValidator.Python.Models;
using TestCases.Models;

namespace SolutionValidator.Python.Interfaces
{
    public interface IPythonTestContextBuilder
    {
        PythonTestContext Build(string sourceCode, List<TestCase> testCases, CodeProblemMethodInfo methodInfo);

        PythonTestContext BuildForSyntaxCheck(string sourceCode, TestCase testCase, CodeProblemMethodInfo methodInfo);
    }
}