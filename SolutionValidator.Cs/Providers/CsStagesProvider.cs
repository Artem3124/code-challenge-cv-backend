using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Extensions;
using SolutionValidator.Cs.Stages;
using SolutionValidators.Core.Interfaces;
using SolutionValidators.Core.Stages;

namespace SolutionValidator.Cs.Providers
{
    public class CsStagesProvider : IStagesProvider
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IStageCreator _stageCreator;

        public CsStagesProvider(IServiceProvider serviceProvider, IStageCreator stageCreator)
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
                _stageCreator.GetStageOrThrow<CsCompilationStage>(scope),
                _stageCreator.GetStageOrThrow<CsTestingStage>(scope),
            };
        }
    }
}
