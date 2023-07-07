using FileScopeProvider.Interfaces;
using Newtonsoft.Json;
using Shared.Core.Extensions;
using SolutionValidators.Core.Interfaces;
using TestCases.Models;

namespace WorkerService.Stager.Services
{
    public class CppTestCaseResultReader : ITestCaseResultReader
    {
        private readonly IFileService _fileService;

        public CppTestCaseResultReader(IFileService fileService)
        {
            _fileService = fileService.ThrowIfNull();
        }

        public List<TestCaseResult> Read(string fileName)
        {
            var content = _fileService.Read(fileName);

            var testCaseResults = JsonConvert.DeserializeObject<List<TestCaseResult>>(content);

            if (testCaseResults is null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            return testCaseResults;
        }
    }
}
