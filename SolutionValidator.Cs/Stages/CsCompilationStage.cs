using Shared.Core.Compilers;
using Shared.Core.Extensions;
using SolutionValidator.Cs.Enums;
using SolutionValidator.Cs.Interfaces;
using SolutionValidator.Cs.Models;
using SolutionValidators.Core.ContextModels;
using SolutionValidators.Core.Stages;

namespace SolutionValidator.Cs.Stages
{
    internal class CsCompilationStage : CompilationStageBase
    {
        private readonly ICsCompilerClient _compilerClient;
        private readonly ICsContextBuilder _cSContextBuilder;
        private readonly IInternalAssemblyPool _internalAssemblyPool;

        public CsCompilationStage(
            ICsCompilerClient compilerClient,
            ICsContextBuilder cSContextBuilder,
            IInternalAssemblyPool internalAssemblyPool)
        {
            _compilerClient = compilerClient.ThrowIfNull();
            _cSContextBuilder = cSContextBuilder.ThrowIfNull();
            _internalAssemblyPool = internalAssemblyPool.ThrowIfNull();
        }

        public override IRunContext Execute(IRunContext context)
        {
            var codeRun = context.CodeRun;
            var methodInfo = context.CodeProblem?.MethodInfo ?? context.Challenge.MethodInfo;
            var compilationContext = _cSContextBuilder.Build(
                codeRun.SourceCode,
                methodInfo,
                testCases: context.TestCases,
                contextBuildOptions: new() { CSContextBuildOptions.AddEntryPoint, CSContextBuildOptions.AddTestCases, CSContextBuildOptions.AddNUnit }
            );

            var compilationResult = _compilerClient.Compile(new CSCompilationRequest(compilationContext, context.CodeProblem?.Name ?? context.Challenge.Name));

            context.Diagnostics = compilationResult.Diagnostics;

            if (compilationResult.Assembly != null)
            {
                context.InternalAssemblyUUID = _internalAssemblyPool.Push(compilationResult.Assembly);
            }

            return new CompilationRunContext(context);
        }
    }
}
