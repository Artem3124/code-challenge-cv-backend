using Microsoft.Extensions.DependencyInjection;
using SolutionValidators.Core.Interfaces;

namespace WorkerService.Stager.Interfaces
{
    internal interface IStageProviderCreator
    {
        IStagesProvider GetStageProviderOrThrow<T>() where T : IStagesProvider;
        IStagesProvider GetStageProviderOrThrow<T>(IServiceScope scope) where T : IStagesProvider;
    }
}