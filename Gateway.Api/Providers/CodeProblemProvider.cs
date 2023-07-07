using CodeProblemAssistant.Contract.Clients;
using CodeRunManager.Contract.Enums;
using CodeRunManager.Contract.Interfaces;
using Gateway.Api.Interfaces;
using Gateway.Api.Mappers;
using Gateway.Contact.Models;
using Shared.Core.Enums;
using Shared.Core.Extensions;

namespace Gateway.Api.Providers
{
    public class CodeProblemProvider : ICodeProblemProvider
    {
        private readonly ICodeProblemAssistantClient _codeProblemAssistantClient;
        private readonly ICodeProblemMapper _codeProblemMapper;
        private readonly ICodeRunManagerClient _codeRunManagerClient;

        public CodeProblemProvider(ICodeProblemAssistantClient codeProblemAssistantClient, ICodeProblemMapper codeProblemMapper, ICodeRunManagerClient codeRunManagerClient)
        {
            _codeProblemAssistantClient = codeProblemAssistantClient.ThrowIfNull();
            _codeProblemMapper = codeProblemMapper.ThrowIfNull();
            _codeRunManagerClient = codeRunManagerClient.ThrowIfNull();
        }

        public async Task<Guid> CreateAsync(CreateCodeProblemRequest request)
        {
            var uuid = await _codeProblemAssistantClient.CreateCodeProblemAsync(new CodeProblemAssistant.Contract.Models.CodeProblemCreateRequest
            {
                Name = request.Name,
                Examples = request.Examples,
                Description = request.Description,
                ComplexityTypeId = request.ComplexityTypeId,
                Constraints = request.Constraints,
                ParameterNames = request.ParameterNames,
                ParameterTypes = request.ParameterTypes,
                Explanation = request.Explanation,
                ReturnType = request.ReturnType,
                TestCaseSetUUID = request.TestCaseSetUUID,
            });

            await _codeProblemAssistantClient.CreateCodeProblemTagBatch(new CodeProblemAssistant.Contract.Models.TagCreateRequestBatch(request.Tags, uuid));

            return uuid;
        }

        public async Task<CodeProblem> Get(Guid uuid)
        {
            var codeProblem = await _codeProblemAssistantClient.GetCodeProblemByUUIDAsync(uuid);

            return _codeProblemMapper.Map(codeProblem);
        }

        public async Task<List<CodeProblem>> GetForUser(Guid userUUID)
        {
            var codeProblems = await _codeProblemAssistantClient.GetCodeProblemsAsync();

            var codeRuns = await _codeRunManagerClient.QueryCodeRunsExpandedAsync(new()
            {
                UserUUID = userUUID,
                RunType = RunType.Submit,
            });

            var result = _codeProblemMapper.Map(codeProblems);

            result.ForEach(p =>
            {
                var relatedRuns = codeRuns.Where(r => r.CodeProblemUUID == p.UUID).ToList();
                if (relatedRuns.Any(r => r.CodeRunOutcomeId == CodeRunOutcome.Succeeded))
                {
                    p.State = CodeProblemState.Resolved;
                }
                else if (relatedRuns.Any())
                {
                    p.State = CodeProblemState.Attended;
                }
                else
                {
                    p.State = CodeProblemState.Unattended;
                }
            });

            return result;
        }
    }
}
