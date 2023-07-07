using CodeProblemAssistant.Contract.Clients;
using CodeRunManager.Contract.Interfaces;
using CodeRunManager.Contract.Models;
using Gateway.Api.Interfaces;
using Gateway.Contact.Models;
using Shared.Core.Enums;
using Shared.Core.Exceptions;
using Shared.Core.Extensions;

namespace Gateway.Api.Services
{
    public class CodeSubmitService : ICodeSubmitService
    {
        private readonly ICodeRunManagerClient _codeRunManagerClient;
        private readonly ICodeProblemAssistantClient _codeProblemAssistantClient;
        private readonly ICodeLanguageTypeValidator _codeLanguageTypeValidator;
        private readonly ICodeRunResultService _codeRunResultService;

        public CodeSubmitService(
            ICodeRunManagerClient codeRunManagerClient,
            ICodeProblemAssistantClient codeProblemAssistantClient,
            ICodeLanguageTypeValidator codeLanguageTypeValidator,
            ICodeRunResultService codeRunResultService)
        {
            _codeRunManagerClient = codeRunManagerClient.ThrowIfNull();
            _codeProblemAssistantClient = codeProblemAssistantClient.ThrowIfNull();
            _codeLanguageTypeValidator = codeLanguageTypeValidator.ThrowIfNull();
            _codeRunResultService = codeRunResultService.ThrowIfNull();
        }

        public async Task<Guid> Submit(CodeSubmitRequest request, Guid userUUID)
        {
            _codeLanguageTypeValidator.Validate(request.CodeLanguage);

            var codeProblem = await _codeProblemAssistantClient.GetCodeProblemByUUIDAsync(request.CodeProblemUUID);
            if (codeProblem == null)
            {
                throw new ObjectNotFoundException(request.CodeProblemUUID, nameof(codeProblem));
            }

            return await _codeRunManagerClient.QueueRunAsync(new CodeRunQueueRequest(userUUID, request.CodeProblemUUID, request.CodeLanguage, request.SourceCode, request.RunType));
        }

        public async Task<Guid> SubmitChallenge(CodeSubmitRequest request, Guid userUUID)
        {
            _codeLanguageTypeValidator.Validate(request.CodeLanguage);

            var challenge = await _codeProblemAssistantClient.GetChallengeAsync(request.CodeProblemUUID, userUUID);
            if (challenge == null)
            {
                throw new ObjectNotFoundException(request.CodeProblemUUID, nameof(challenge));
            }

            return await _codeRunManagerClient.QueueRunAsync(new CodeRunQueueRequest(userUUID, request.CodeProblemUUID, request.CodeLanguage, request.SourceCode, request.RunType));
        }

        public async Task<CodeRunProgress> GetProgress(Guid userUUID, Guid runUUID)
        {
            var state = new CodeRunProgress
            {
                Stage = await _codeRunManagerClient.GetStageAsync(runUUID)
            };
            if (state.Stage == CodeRunStage.Completed)
            {
                state.Result = await _codeRunResultService.GetByCodeCodeRunUUIDAsync(userUUID, runUUID);
            }

            return state;
        }
    }
}
