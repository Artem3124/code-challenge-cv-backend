using TestCases.Models;

namespace SolutionValidators.Core.Interfaces
{
    public interface ITestCaseResultReader
    {
        List<TestCaseResult> Read(string fileName);
    }
}