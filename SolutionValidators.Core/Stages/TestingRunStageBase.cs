using Shared.Core.Enums;
using SolutionValidators.Core.ContextModels;

namespace SolutionValidators.Core.Stages
{
    public abstract class TestingRunStageBase : IRunStage
    {
        public CodeRunStage Stage { get; init; }

        public abstract IRunContext Execute(IRunContext context);

        public TestingRunStageBase()
        {
            Stage = CodeRunStage.Testing;
        }
    }
}
