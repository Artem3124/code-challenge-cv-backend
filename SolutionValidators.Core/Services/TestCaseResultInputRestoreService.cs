using SolutionValidators.Core.Interfaces;
using TestCases.Models;

namespace WorkerService.Stager.Services
{
    public class TestCaseResultInputRestoreService : ITestCaseResultInputRestoreService
    {
        public List<TestCaseResult> Restore(List<TestCaseResult> testCaseResults, List<TestCase> testCases)
        {
            var testCasesDictionary = testCases.ToDictionary(t => t.Id);

            testCaseResults.ForEach(r =>
            {
                r.Inputs = string.Join(',', testCases[r.Id].Input);
            });

            return testCaseResults;
        }
    }
}
