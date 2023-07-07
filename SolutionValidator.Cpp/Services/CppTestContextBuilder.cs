using CodeProblemAssistant.Contract.Models;
using Microsoft.Extensions.Options;
using Shared.Core.Extensions;
using Shared.Core.Interfaces;
using SolutionValidator.Cpp.Interfaces;
using SolutionValidator.Cpp.Models;
using TestCases.Models;

namespace SolutionValidator.Cpp.Services
{
    internal class CppTestContextBuilder : ICppTestContextBuilder
    {
        private readonly string _fileNamePrefix;
        private readonly string _fileNamePostfix;
        private readonly string _testLibraryPath;
        private readonly string _testLibraryObjectFilePath;
        private readonly CppCodeAutocomplitionService _codeAutocomplitionService;
        private readonly IIncludeService _includeService;
        private readonly ICppIncludesProvider _cppIncludesProvider;
        private readonly IFileNameGenerator _fileNameGenerator;

        public CppTestContextBuilder(
            CppCodeAutocomplitionService codeAutocomplitionService,
            IIncludeService includeService,
            ICppIncludesProvider cppIncludesProvider,
            IFileNameGenerator fileNameGenerator,
            IOptions<CppTestContextBuilderSettings> cppTestContextBuilderSettings)
        {
            _codeAutocomplitionService = codeAutocomplitionService.ThrowIfNull();
            _includeService = includeService.ThrowIfNull();
            _cppIncludesProvider = cppIncludesProvider.ThrowIfNull();
            _fileNameGenerator = fileNameGenerator.ThrowIfNull();

            var options = cppTestContextBuilderSettings.Value.ThrowIfNull();
            _fileNamePrefix = options.FileNamePrefix.ThrowIfNull();
            _fileNamePostfix = options.FileNamePostfix.ThrowIfNull();
            _testLibraryPath = options.TestLibraryEntryPointPath.ThrowIfNull();
            _testLibraryObjectFilePath = options.TestLibraryObjectFilePath.ThrowIfNull();
        }

        public CppCompilationContext Build(string sourceCode, CodeProblemMethodInfo methodInfo, List<TestCase> testCases)
        {
            var testingFileContent = _codeAutocomplitionService.WrapForTesting(string.Empty, methodInfo, testCases);
            var sourceCodeFile = new HeaderFile(GenerateFileName(), sourceCode);
            var testingFileName = GenerateFileName();
            var testingFile = new SourceFile(testingFileName,
                $"{_codeAutocomplitionService.GetInternalInclude(sourceCodeFile.Name)}\n" +
                $"{testingFileContent}");

            var files = new List<CppFile>
            {
                sourceCodeFile,
            };

            var context = new CppCompilationContext(files, testingFile);

            var includes = _cppIncludesProvider.Get();

            includes.Add(new Include(_testLibraryPath, IncludeType.Internal, _testLibraryObjectFilePath));

            return _includeService.AddIncludes(context, GenerateFileName(), includes);
        }

        private string GenerateFileName() => _fileNameGenerator.Generate(_fileNamePrefix, _fileNamePostfix);
    }
}