using Microsoft.Extensions.DependencyInjection;
using SolutionValidators.Core.Stages;

namespace SolutionValidators.Core.Interfaces
{
    public interface IStageCreator
    {
        IRunStage GetStageOrThrow<T>() where T : IRunStage;
        IRunStage GetStageOrThrow<T>(IServiceScope scope) where T : IRunStage;
    }
}