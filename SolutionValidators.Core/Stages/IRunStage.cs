using Shared.Core.Enums;
using SolutionValidators.Core.ContextModels;

namespace SolutionValidators.Core.Stages
{
    public interface IRunStage
    {
        public CodeRunStage Stage { get; init; }

        public IRunContext Execute(IRunContext context);
    }
}
