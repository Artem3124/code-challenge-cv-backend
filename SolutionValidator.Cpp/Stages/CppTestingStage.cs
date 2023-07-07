using Shared.Core.Compilers;
using Shared.Core.Extensions;
using SolutionValidators.Core.ContextModels;
using SolutionValidators.Core.Interfaces;
using SolutionValidators.Core.Stages;

namespace SolutionValidator.Cpp.Stages
{
    internal class CppTestingStage : TestingRunStageBase
    {
        private readonly IInternalAssemblyPool _assemblyPool;
        private readonly ITestCaseResultReader _testCaseResultReader;
        private readonly ITestCaseResultInputRestoreService _inputRestoreService;

        private readonly string _testFileName = "TestResutls.json";

        public CppTestingStage(IInternalAssemblyPool assemblyPool, ITestCaseResultReader testCaseResultReader, ITestCaseResultInputRestoreService inputRestoreService)
        {
            _assemblyPool = assemblyPool.ThrowIfNull();
            _testCaseResultReader = testCaseResultReader.ThrowIfNull();
            _inputRestoreService = inputRestoreService.ThrowIfNull();
        }

        public override IRunContext Execute(IRunContext context)
        {
            using var assembly = _assemblyPool.Pop(context.InternalAssemblyUUID);

            if (context.TestCases?.Any() != true)
            {
                context.TestCaseResults = new();
                return new TestingRunContext(context);
            }

            assembly.Execute();

            var testCasesResults = _testCaseResultReader.Read(_testFileName);

            context.TestCaseResults = _inputRestoreService.Restore(testCasesResults, context.TestCases);

            return new TestingRunContext(context);
        }
    }
}
