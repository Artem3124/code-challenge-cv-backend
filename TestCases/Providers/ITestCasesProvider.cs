using TestCases.Models;

namespace TestCases.Providers
{
    public interface ITestCasesProvider
    {
        public List<TestCase> GetTestCaseSet(Guid testCaseSetUUID, int? count = default);
    }
}
