using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Extensions;
using SolutionValidators.Core.Interfaces;
using WorkerService.Stager.Interfaces;

namespace WorkerService.Stager.Services
{
    internal class StageProviderCreator : IStageProviderCreator
    {
        private readonly IServiceProvider _serviceProvider;

        public StageProviderCreator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider.ThrowIfNull();
        }

        public IStagesProvider GetStageProviderOrThrow<T>() where T : IStagesProvider
        {
            var scope = _serviceProvider.CreateScope();
            var stage = scope.ServiceProvider.GetService<T>();
            if (stage == null)
            {
                throw new ArgumentException($"Unable to find stage for {typeof(T)}");
            }

            return stage;
        }

        public IStagesProvider GetStageProviderOrThrow<T>(IServiceScope scope) where T : IStagesProvider
        {
            var stage = scope.ServiceProvider.GetService<T>();
            if (stage == null)
            {
                throw new ArgumentException($"Unable to find stage for {typeof(T)}");
            }

            return stage;
        }
    }
}
