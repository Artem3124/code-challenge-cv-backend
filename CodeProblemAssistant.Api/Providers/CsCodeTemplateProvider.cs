using CodeProblemAssistant.Api.Mappers;
using CodeProblemAssistant.Data;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Exceptions;
using Shared.Core.Extensions;
using Shared.Core.Interfaces;
using SolutionValidator.Cs.Interfaces;

namespace Cs.CodeAutocompletion.Providers
{
    public class CsCodeTemplateProvider : ICodeTemplateProvider
    {
        private readonly ICodeAutocompletionService _codeAutocompletionService;
        private readonly CodeProblemAssistantContext _db;
        private readonly CodeProblemMethodInfoMapper _mapper;

        public CsCodeTemplateProvider(ICodeAutocompletionService codeAutocompletionService, CodeProblemAssistantContext db, CodeProblemMethodInfoMapper mapper)
        {
            _codeAutocompletionService = codeAutocompletionService.ThrowIfNull();
            _db = db.ThrowIfNull();
            _mapper = mapper.ThrowIfNull();
        }

        public async Task<string> GetChallengeTemplate(Guid challengeUUID, CancellationToken cancellationToken = default)
        {
            var challenge = await _db.Challenges
                .Where(p => p.UUID == challengeUUID)
                .FirstOrDefaultAsync(cancellationToken);
            if (challenge == null)
            {
                throw new ObjectNotFoundException(challengeUUID, nameof(challenge));
            }

            return _codeAutocompletionService.GetSolutionTemplate(_mapper.Map(challenge));
        }

        public async Task<string> GetSolutionTemplate(Guid codeProblemUUID, CancellationToken cancellationToken = default)
        {
            var codeProblem = await _db.CodeProblems
                .Where(p => p.UUID == codeProblemUUID)
                .FirstOrDefaultAsync(cancellationToken);
            if (codeProblem == null)
            {
                throw new ObjectNotFoundException(codeProblemUUID, nameof(codeProblem));
            }

            return _codeAutocompletionService.GetSolutionTemplate(_mapper.Map(codeProblem));
        }
    }
}
