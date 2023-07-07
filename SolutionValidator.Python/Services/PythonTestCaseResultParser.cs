using TestCases.Models;

namespace SolutionValidator.Python.Services
{
    internal class PythonTestCaseResultParser
    {
        public List<TestCaseResult> Parse(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return new();
            }

            var lines = value.Split('\n');
            if (!lines[0].Contains('F', StringComparison.OrdinalIgnoreCase) && !lines[0].Contains('E', StringComparison.OrdinalIgnoreCase))
            {
                return new();
            }

            // At least it works...
            var testCaseResult = new TestCaseResult
            {
                Id = int.Parse(lines[2].Split(' ')[1].Split('_')[1]),
                Result = "failed",
                Message = lines[7].Replace("AssertionError:", string.Empty),
            };

            return new() { testCaseResult };
        }
    }
}
