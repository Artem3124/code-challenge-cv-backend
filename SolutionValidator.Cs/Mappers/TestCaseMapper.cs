using Shared.Core.Interfaces;
using SolutionValidator.Cs.Parsers.XmlModels;
using TestCases.Models;
using Xml = SolutionValidator.Cs.Parsers.XmlModels;
namespace SolutionValidator.Cs.Mappers
{
    public interface ITestCaseMapper : IMapper<Xml.TestCase, TestCaseResult>
    {

    }

    public class TestCaseMapper : ITestCaseMapper
    {
        private static readonly string _expected = "Expected: ";
        private static readonly string _actual = "But was: ";

        public TestCaseResult Map(Xml.TestCase entity)
        {
            return new TestCaseResult(GetInput(entity), GetFromMessage(entity, _expected), GetFromMessage(entity, _actual), GetResult(entity.Result), entity.Failure?.Message?.Trim(), int.Parse(entity.Id));
        }

        public List<TestCaseResult> Map(List<Xml.TestCase> entity)
        {
            return entity.Select(e => Map(e)).ToList();
        }

        private string GetInput(Xml.TestCase testCase)
        {
            return testCase.Name.Replace($"{testCase.ClassName}.{testCase.MethodName}", string.Empty);
        }

        private string GetResult(ResultType result) => result switch
        {
            ResultType.Failed => "failed",
            ResultType.Passed => "succeeded",
            _ => string.Empty,
        };

        private string GetFromMessage(Xml.TestCase testCase, string what)
        {
            if (string.IsNullOrEmpty(testCase.Failure?.Message))
            {
                return string.Empty;
            }
            var indexOfExpected = testCase.Failure.Message.IndexOf(what) + what.Length;

            return string.Join(string.Empty, testCase.Failure.Message.Skip(indexOfExpected).TakeWhile(m => m != '\n'));
        }
    }
}
