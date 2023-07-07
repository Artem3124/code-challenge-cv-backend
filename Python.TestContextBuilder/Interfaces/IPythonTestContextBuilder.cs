using CodeProblemAssistant.Contract.Models;
using Python.TestContextBuilder.Models;
using TestCases.Models;

namespace Python.TestContextBuilder.Interfaces
{
    public interface IPythonTestContextBuilder
    {
        PythonTestContext Build(string sourceCode, List<TestCase> testCases, CodeProblemMethodInfo methodInfo);

        PythonTestContext BuildForSyntaxCheck(string sourceCode, TestCase testCase, CodeProblemMethodInfo methodInfo);
    }
}