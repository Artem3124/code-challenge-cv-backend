using FileScopeProvider.Interfaces;
using Shared.Core.Enums;
using Shared.Core.Extensions;
using SolutionValidator.Python.Interfaces;
using SolutionValidators.Core.ContextModels;
using SolutionValidators.Core.Stages;
using SolutionValidator.Python.Services;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("SolutionValidator.Python.Tests")]
namespace SolutionValidator.Python.Stages
{
    internal class PythonSyntaxCheckStage : IRunStage
    {
        private readonly IPythonScriptRunner _scriptRunner;
        private readonly IFileScopeProvider _fileScopeProvider;
        private readonly PythonSyntaxCheckResultParser _syntaxCheckResultParser;
        private readonly IPythonTestContextBuilder _testContextBuilder;

        public CodeRunStage Stage { get; init; } = CodeRunStage.Queued;

        public PythonSyntaxCheckStage(
            IPythonScriptRunner scriptRunner,
            IFileScopeProvider fileScopeProvider,
            IPythonTestContextBuilder testContextBuilder,
            PythonSyntaxCheckResultParser syntaxCheckResultParser)
        {
            _scriptRunner = scriptRunner.ThrowIfNull();
            _fileScopeProvider = fileScopeProvider.ThrowIfNull();
            _syntaxCheckResultParser = syntaxCheckResultParser.ThrowIfNull();
            _testContextBuilder = testContextBuilder.ThrowIfNull();
        }

        public IRunContext Execute(IRunContext context)
        {
            var methodInfo = context.CodeProblem?.MethodInfo ?? context.Challenge.MethodInfo;
            var syntaxCheckContext = _testContextBuilder.BuildForSyntaxCheck(context.CodeRun.SourceCode, context.TestCases.First(), methodInfo);
            using var fileScope = _fileScopeProvider.Create(new(syntaxCheckContext.Files));

            var output = _scriptRunner.Run(syntaxCheckContext.EntryPointFileName, false);

            context.Diagnostics = _syntaxCheckResultParser.Parse(output.StandardError ?? string.Empty);

            return new CompilationRunContext(context);
        }
    }
}
