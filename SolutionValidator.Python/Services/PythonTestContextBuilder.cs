using CodeProblemAssistant.Contract.Models;
using Shared.Core.Interfaces;
using SolutionValidator.Python.Interfaces;
using SolutionValidator.Python.Models;
using TestCases.Models;

namespace SolutionValidator.Python.Services
{
    internal class PythonTestContextBuilder : IPythonTestContextBuilder
    {
        private readonly PythonCodeAutocomplitionService _codeAutocomplitionService;
        private readonly IFileNameGenerator _fileNameGenerator;

        public PythonTestContextBuilder(PythonCodeAutocomplitionService codeAutocomplitionService, IFileNameGenerator fileNameGenerator)
        {
            _codeAutocomplitionService = codeAutocomplitionService;
            _fileNameGenerator = fileNameGenerator;
        }

        public PythonTestContext Build(string sourceCode, List<TestCase> testCases, CodeProblemMethodInfo methodInfo)
        {
            var solutionCodeFileName = _fileNameGenerator.Generate();
            var testingFileName = _fileNameGenerator.Generate();
            var testsFileContent = _codeAutocomplitionService.WrapForTesting(testCases, methodInfo, solutionCodeFileName);

            var files = new List<PythonFile>
            {
                new (sourceCode, solutionCodeFileName),
                new (testsFileContent, testingFileName),
            };

            return new PythonTestContext(files, testingFileName);
        }

        public PythonTestContext BuildForSyntaxCheck(string sourceCode, TestCase testCase, CodeProblemMethodInfo methodInfo)
        {
            var fileName = _fileNameGenerator.Generate();
            var file = new PythonFile($"{sourceCode}\n{_codeAutocomplitionService.WrapForSyntaxCheck(testCase, methodInfo)}", fileName);

            return new(new() { file }, fileName);
        }
    }
}
