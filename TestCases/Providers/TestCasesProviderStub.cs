using TestCases.Models;

namespace TestCases.Providers
{
    public class TestCasesProviderStub : ITestCasesProvider
    {
        public List<TestCase> GetTestCaseSet(Guid testCaseSetUUID, int? count = default)
        {
            return new List<TestCase>
            {
                new TestCase(new() { "a, b, c" ,"b" }, "b", 0),
                new TestCase(new() { "c, b, c" ,"c" }, "c", 1),
                new TestCase(new() { "q, a, q","q"}, "q", 2),
            };
        }
    }
}
