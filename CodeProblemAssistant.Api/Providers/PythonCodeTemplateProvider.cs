using CodeProblemAssistant.Api.Mappers;
using CodeProblemAssistant.Data;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Exceptions;
using Shared.Core.Extensions;
using Shared.Core.Interfaces;
using SolutionValidator.Python.Services;

namespace CodeProblemAssistant.Api.Providers
{
    public class PythonCodeTemplateProvider : ICodeTemplateProvider
    {
        private readonly CodeProblemAssistantContext _db;
        private readonly PythonCodeAutocomplitionService _codeAutocomplitionService;
        private readonly CodeProblemMethodInfoMapper _mapper;

        public PythonCodeTemplateProvider(
            CodeProblemAssistantContext db,
            PythonCodeAutocomplitionService codeAutocomplitionService,
            CodeProblemMethodInfoMapper mapper)
        {
            _db = db.ThrowIfNull();
            _codeAutocomplitionService = codeAutocomplitionService.ThrowIfNull();
            _mapper = mapper.ThrowIfNull();
        }

        public async Task<string> GetChallengeTemplate(Guid challengeUUID, CancellationToken cancellationToken = default)
        {
            var challenge = await _db.Challenges.FirstOrDefaultAsync(p => p.UUID == challengeUUID, cancellationToken);

            if (challenge == null)
            {
                throw new ObjectNotFoundException(challengeUUID, nameof(challenge));
            }

            return _codeAutocomplitionService.GetSolutionTemplate(_mapper.Map(challenge));
        }

        public async Task<string> GetSolutionTemplate(Guid codeProblemUUID, CancellationToken cancellationToken = default)
        {
            var codeProblem = await _db.CodeProblems.FirstOrDefaultAsync(p => p.UUID == codeProblemUUID, cancellationToken);

            if (codeProblem == null)
            {
                throw new ObjectNotFoundException(codeProblemUUID, nameof(codeProblem));
            }

            return _codeAutocomplitionService.GetSolutionTemplate(_mapper.Map(codeProblem));
        }
    }
}
