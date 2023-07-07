using Shared.Core.Extensions;
using SolutionValidator.Cs.Mappers;
using SolutionValidator.Cs.Parsers;
using SolutionValidator.Cs.Parsers.XmlModels;
using SolutionValidators.Core.Interfaces;
using TestCases.Models;

namespace SolutionValidator.Cs.Services
{
    public class CsTestCaseResultReader : ITestCaseResultReader
    {
        private readonly XmlParser _xmlParser;
        private readonly ITestCaseMapper _mapper;

        public CsTestCaseResultReader(ITestCaseMapper mapper, XmlParser xmlParser)
        {
            _mapper = mapper.ThrowIfNull();
            _xmlParser = xmlParser.ThrowIfNull();
        }

        public List<TestCaseResult> Read(string fileName)
        {
            var testRun = _xmlParser.ParseFile<TestRun>(fileName);
            if (testRun == null)
            {
                throw new InvalidOperationException("Test file does not exists.");
            }
            var testSuite = testRun.TestSuite;
            while (testSuite.NestedTestSuite != null)
            {
                testSuite = testSuite.NestedTestSuite;
            }

            return _mapper.Map(testSuite.TestCases);
        }
    }
}
