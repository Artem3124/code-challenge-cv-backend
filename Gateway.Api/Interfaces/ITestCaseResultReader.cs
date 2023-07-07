using TestCases.Models;

namespace CodeRunManager.Api.Interfaces
{
    public interface ITestCaseResultReader
    {
        List<TestCaseResult> Read(string fileName);
    }
}