using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Extensions;
using SolutionValidator.Python.Stages;
using SolutionValidators.Core.Interfaces;
using SolutionValidators.Core.Stages;

namespace SolutionValidator.Python.Providers
{
    public class PythonStagesProvider : IStagesProvider
    {
        private readonly IStageCreator _stageCreator;
        private readonly IServiceProvider _serviceProvider;

        public PythonStagesProvider(IStageCreator stageCreator, IServiceProvider serviceProvider)
        {
            _stageCreator = stageCreator.ThrowIfNull();
            _serviceProvider = serviceProvider.ThrowIfNull();
        }

        public IEnumerable<IRunStage> Get()
        {
            var scope = _serviceProvider.CreateScope();

            return new List<IRunStage>
            {
                _stageCreator.GetStageOrThrow<InitialStage>(scope),
                _stageCreator.GetStageOrThrow<PythonSyntaxCheckStage>(scope),
                _stageCreator.GetStageOrThrow<PythonTestingStage>(scope),
            };
        }
    }
}
