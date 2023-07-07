using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Extensions;
using SolutionValidators.Core.Interfaces;
using SolutionValidators.Core.Stages;

namespace SolutionValidators.Core.Services
{
    public class StageCreator : IStageCreator
    {
        private readonly IServiceProvider _serviceProvider;

        public StageCreator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider.ThrowIfNull();
        }

        public IRunStage GetStageOrThrow<T>() where T : IRunStage
        {
            var scope = _serviceProvider.CreateScope();
            var stage = scope.ServiceProvider.GetService<T>();
            if (stage == null)
            {
                throw new ArgumentException($"Unable to find stage for {typeof(T)}");
            }

            return stage;
        }

        public IRunStage GetStageOrThrow<T>(IServiceScope scope) where T : IRunStage
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
