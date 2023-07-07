using Shared.Core.Enums;
using SolutionValidators.Core.Stages;

namespace WorkerService.Stager.Interfaces
{
    internal interface IStagePipelineBuilder
    {
        IEnumerable<IRunStage> Build(CodeLanguage codeLanguage);
    }
}