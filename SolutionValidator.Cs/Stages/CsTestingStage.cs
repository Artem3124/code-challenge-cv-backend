using Shared.Core.Compilers;
using Shared.Core.Extensions;
using SolutionValidators.Core.ContextModels;
using SolutionValidators.Core.Interfaces;
using SolutionValidators.Core.Stages;

namespace SolutionValidator.Cs.Stages
{
    internal class CsTestingStage : TestingRunStageBase
    {
        private readonly IInternalAssemblyPool _assemblyPool;
        private readonly ITestCaseResultReader _testCaseResultReader;
        private readonly string _testResultFileName = "TestResult.xml";

        public CsTestingStage(IInternalAssemblyPool assemblyPool, ITestCaseResultReader testCaseResultReader)
        {
            _assemblyPool = assemblyPool.ThrowIfNull();
            _testCaseResultReader = testCaseResultReader.ThrowIfNull();
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

            context.TestCaseResults = _testCaseResultReader.Read(_testResultFileName);

            return new TestingRunContext(context);
        }
    }
}
