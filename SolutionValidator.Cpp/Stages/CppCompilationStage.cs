using Shared.Core.Compilers;
using Shared.Core.Extensions;
using SolutionValidator.Cpp.Interfaces;
using SolutionValidators.Core.ContextModels;
using SolutionValidators.Core.Stages;

namespace SolutionValidator.Cpp.Stages
{
    internal class CppCompilationStage : CompilationStageBase
    {
        private readonly ICppCompilerAdapter _compiler;
        private readonly IInternalAssemblyPool _assemblyPool;
        private readonly ICppTestContextBuilder _testContextBuilder;

        public CppCompilationStage(
            ICppCompilerAdapter compiler,
            IInternalAssemblyPool assemblyPool,
            ICppTestContextBuilder testContextBuilder)
        {
            _compiler = compiler.ThrowIfNull();
            _assemblyPool = assemblyPool.ThrowIfNull();
            _testContextBuilder = testContextBuilder.ThrowIfNull();
        }

        public override IRunContext Execute(IRunContext context)
        {
            var methodInfo = context.CodeProblem?.MethodInfo ?? context.Challenge.MethodInfo;
            var compilationContext = _testContextBuilder.Build(context.CodeRun.SourceCode, methodInfo, context.TestCases);

            var compilationResult = _compiler.Compile(new(context.CodeProblem?.Name ?? context.Challenge.Name, compilationContext));
            if (compilationResult.Assembly != null)
            {
                context.InternalAssemblyUUID = _assemblyPool.Push(compilationResult.Assembly);
            }

            context.Diagnostics = compilationResult.Diagnostics.Where(d => d.Column != -1 && d.Line != -1).ToList();

            return new CompilationRunContext(context);
        }
    }
}
