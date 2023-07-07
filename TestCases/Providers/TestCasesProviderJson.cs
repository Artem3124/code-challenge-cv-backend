using FileScopeProvider.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Shared.Core.Exceptions;
using Shared.Core.Extensions;
using TestCases.Models;
using TestCases.Settings;

namespace TestCases.Providers
{
    internal class TestCasesProviderJson : ITestCasesProvider
    {
        private readonly IFileService _fileService;
        private readonly string _folder = "../TestCases/TestCases";

        public TestCasesProviderJson(IFileService fileService, IOptions<TestCaseProviderSettings> options)
        {
            _fileService = fileService.ThrowIfNull();
            _folder = options.Value.TestCasesJsonFolder;
        }

        public List<TestCase> GetTestCaseSet(Guid testCaseSetUUID, int? count = null)
        {
            List<TestCase>? testCases;
            try
            {
                var testCasesJson = _fileService.Read(GetFileName(testCaseSetUUID));

                testCases = JsonConvert.DeserializeObject<List<TestCase>>(testCasesJson);
                if (testCases == null)
                {
                    throw new ObjectNotFoundException(testCaseSetUUID, nameof(testCases));
                }
            }
            catch (Exception ex) when (ex.Message.Contains("file") || ex.Message.Contains("path"))
            {
                return new List<TestCase>();
            }

            return count.HasValue && testCases.Count > count
                ? testCases.Take(count.Value).ToList()
                : testCases;
        }

        private string GetFileName(Guid testCaseSetUUID) => $"{_folder}/{testCaseSetUUID}.json";
    }
}
