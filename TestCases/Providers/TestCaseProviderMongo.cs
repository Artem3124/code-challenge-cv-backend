using Mongo.Data.Services;
using Shared.Core.Extensions;
using TestCases.Models;

namespace TestCases.Providers
{
    internal class TestCaseProviderMongo : ITestCasesProvider
    {
        private readonly ITestCaseService _testCaseService;

        public TestCaseProviderMongo(ITestCaseService testCaseService)
        {
            _testCaseService = testCaseService.ThrowIfNull();
        }

        public List<TestCase> GetTestCaseSet(Guid testCaseSetUUID, int? count = default)
        {
            var testCases = _testCaseService.GetTestCasesAsync(testCaseSetUUID).GetAwaiter().GetResult();

            if (count.HasValue && testCases.Count > count)
            {
                testCases = testCases.Take(count.Value).ToList();
            }

            return testCases.Select(t => new TestCase(t.Input, t.Expected, t.Id)).ToList();
        }
    }
}
