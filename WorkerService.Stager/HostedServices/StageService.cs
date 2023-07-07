using CodeRunManager.Contract.Interfaces;
using CodeRunManager.Contract.Models;
using Microsoft.Extensions.Logging;
using Mongo.Data.Models;
using Shared.Core.Enums;
using Shared.Core.Extensions;
using SolutionValidators.Core.ContextModels;
using SolutionValidators.Core.Exceptions;
using WorkerService.Stager.Interfaces;

namespace WorkerService.SolutionValidator.HostedServices
{
    internal class StageService : IStageService
    {
        private readonly IStagePipelineBuilder _stagePipelineBuilder;
        private readonly ICodeRunManagerClient _codeRunManagerClient;
        private readonly ILogger<StageService> _logger;

        public StageService(IStagePipelineBuilder stagePipelineBuilder, ILogger<StageService> logger, ICodeRunManagerClient codeRunManagerClient)
        {
            _stagePipelineBuilder = stagePipelineBuilder.ThrowIfNull();
            _logger = logger.ThrowIfNull();
            _codeRunManagerClient = codeRunManagerClient.ThrowIfNull();
        }

        public async Task Start(CodeRunQueueMessage codeRun, CancellationToken cancellationToken = default)
        {
            var stages = _stagePipelineBuilder.Build(codeRun.CodeLanguageId);

            IRunContext context = new CodeRunContext(codeRun);

            foreach (var stage in stages)
            {
                try
                {
                    await _codeRunManagerClient.UpdateCodeRunStageAsync(context.CodeRun.UUID, new(stage.Stage), cancellationToken);
                    context = stage.Execute(context);
                    context.Validate();
                }
                catch (CodeRunException ex)
                {
                    await CompleteRun(ex.Reason, context.CodeRun.UUID, ex.Metadata);
                    return;
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex, "Unexpected error occurred while running {codeRunUUID}", codeRun.UUID);

                    await CompleteRun(CodeRunOutcome.Unknown, context.CodeRun.UUID);

                    return;
                }
            }

            await CompleteRun(CodeRunOutcome.Succeeded, context.CodeRun.UUID);
        }

        private Task CompleteRun(CodeRunOutcome outcome, Guid codeRunUUID, Dictionary<string, string>? metadata = default)
        {
            var request = new CodeRunCompleteRequest(outcome, metadata);

            return _codeRunManagerClient.CompleteCodeRunAsync(codeRunUUID, request);
        }
    }
}
