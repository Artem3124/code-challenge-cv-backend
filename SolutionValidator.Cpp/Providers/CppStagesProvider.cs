using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Extensions;
using SolutionValidator.Cpp.Stages;
using SolutionValidators.Core.Interfaces;
using SolutionValidators.Core.Stages;

namespace SolutionValidator.Cpp.Providers
{
    public class CppStagesProvider : IStagesProvider
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IStageCreator _stageCreator;

        public CppStagesProvider(IServiceProvider serviceProvider, IStageCreator stageCreator)
        {
            _serviceProvider = serviceProvider.ThrowIfNull();
            _stageCreator = stageCreator.ThrowIfNull();
        }

        public IEnumerable<IRunStage> Get()
        {
            var scope = _serviceProvider.CreateScope();
            return new List<IRunStage>
            {
                _stageCreator.GetStageOrThrow<InitialStage>(scope),
                _stageCreator.GetStageOrThrow<CppCompilationStage>(scope),
                _stageCreator.GetStageOrThrow<CppTestingStage>(scope),
            };
        }
    }
}
