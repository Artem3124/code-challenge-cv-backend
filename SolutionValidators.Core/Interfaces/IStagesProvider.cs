using SolutionValidators.Core.Stages;

namespace SolutionValidators.Core.Interfaces
{
    public interface IStagesProvider
    {
        IEnumerable<IRunStage> Get();
    }
}
