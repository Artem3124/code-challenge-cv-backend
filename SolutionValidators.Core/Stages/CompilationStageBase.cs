using Shared.Core.Enums;
using SolutionValidators.Core.ContextModels;

namespace SolutionValidators.Core.Stages
{
    public abstract class CompilationStageBase : IRunStage
    {
        public CodeRunStage Stage { get; init; }

        public abstract IRunContext Execute(IRunContext context);

        public CompilationStageBase()
        {
            Stage = CodeRunStage.Compiling;
        }
    }
}
