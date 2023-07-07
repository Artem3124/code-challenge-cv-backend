using FileScopeProvider.Interfaces;
using FileScopeProvider.Models;
using Shared.Core.Extensions;
using SolutionValidator.Python.Interfaces;
using SolutionValidator.Python.Services;
using SolutionValidators.Core.ContextModels;
using SolutionValidators.Core.Interfaces;
using SolutionValidators.Core.Stages;

namespace SolutionValidator.Python.Stages
{
    internal class PythonTestingStage : TestingRunStageBase
    {
        private readonly IPythonTestContextBuilder _pythonTestContextBuilder;
        private readonly IPythonScriptRunner _pythonScriptRunner;
        private readonly IFileScopeProvider _fileScopeProvider;
        private readonly ITestCaseResultInputRestoreService _inputRestoreService;
        private readonly PythonTestCaseResultParser _testCaseResultParser;

        public PythonTestingStage(
            IPythonTestContextBuilder pythonTestContextBuilder,
            IPythonScriptRunner pythonScriptRunner,
            IFileScopeProvider fileScopeProvider,
            ITestCaseResultInputRestoreService inputRestoreService,
            PythonTestCaseResultParser testCaseResultParser)
        {
            _pythonTestContextBuilder = pythonTestContextBuilder.ThrowIfNull();
            _pythonScriptRunner = pythonScriptRunner.ThrowIfNull();
            _fileScopeProvider = fileScopeProvider.ThrowIfNull();
            _inputRestoreService = inputRestoreService.ThrowIfNull();
            _testCaseResultParser = testCaseResultParser.ThrowIfNull();
        }

        public override IRunContext Execute(IRunContext context)
        {
            if (context.TestCases?.Any() != true)
            {
                context.TestCaseResults = new();
                return new TestingRunContext(context);
            }

            var methodInfo = context.CodeProblem?.MethodInfo ?? context.Challenge.MethodInfo;
            var testingContext = _pythonTestContextBuilder.Build(context.CodeRun.SourceCode, context.TestCases, methodInfo);
            using var fileScope = _fileScopeProvider.Create(new List<SystemFile>(testingContext.Files));
            var output = _pythonScriptRunner.Run(testingContext.EntryPointFileName, true);

            if (!string.IsNullOrWhiteSpace(output.StandardError))
            {
                var testCaseResults = _testCaseResultParser.Parse(output.StandardError);
                context.TestCaseResults = _inputRestoreService.Restore(testCaseResults, context.TestCases);
            }

            return new TestingRunContext(context);
        }
    }
}
