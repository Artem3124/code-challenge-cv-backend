using TestCases.Models;

namespace SolutionValidators.Core.Interfaces
{
    public interface ITestCaseResultInputRestoreService
    {
        List<TestCaseResult> Restore(List<TestCaseResult> testCaseResults, List<TestCase> testCases);
    }
}