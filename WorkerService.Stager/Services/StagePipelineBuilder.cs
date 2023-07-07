using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Enums;
using Shared.Core.Exceptions;
using Shared.Core.Extensions;
using SolutionValidator.Cpp.Providers;
using SolutionValidator.Cs.Providers;
using SolutionValidator.Python.Providers;
using SolutionValidators.Core.Stages;
using WorkerService.Stager.Interfaces;

namespace WorkerService.Stager.Services
{
    internal class StagePipelineBuilder : IStagePipelineBuilder
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<CodeLanguage, IEnumerable<IRunStage>> _savedStages;
        private readonly IStageProviderCreator _stageProviderCreator;

        public StagePipelineBuilder(IServiceProvider serviceProvider, IStageProviderCreator stageProviderCreator)
        {
            _serviceProvider = serviceProvider.ThrowIfNull();
            _stageProviderCreator = stageProviderCreator.ThrowIfNull();
            _savedStages = new();
        }

        public IEnumerable<IRunStage> Build(CodeLanguage codeLanguage)
        {
            if (_savedStages.TryGetValue(codeLanguage, out var stages))
            {
                return stages;
            }

            var scope = _serviceProvider.CreateScope();
            _savedStages[codeLanguage] = codeLanguage switch
            {
                CodeLanguage.Cs => _stageProviderCreator.GetStageProviderOrThrow<CsStagesProvider>(scope).Get(),
                CodeLanguage.Cpp => _stageProviderCreator.GetStageProviderOrThrow<CppStagesProvider>(scope).Get(),
                CodeLanguage.Python => _stageProviderCreator.GetStageProviderOrThrow<PythonStagesProvider>(scope).Get(),
                _ => throw new CodeLanguageNotSupportedException(codeLanguage.ToString()),
            };

            return _savedStages[codeLanguage];
        }
    }
}
