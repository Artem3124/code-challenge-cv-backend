using CodeProblemAssistant.Api.Interfaces;
using CodeProblemAssistant.Contract.Models;
using CodeProblemAssistant.Data;
using DataModels = CodeProblemAssistant.Data.Models;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Exceptions;
using Shared.Core.Extensions;
using CodeProblemAssistant.Api.Mappers;

namespace CodeProblemAssistant.Api.Services
{
    public class CodeProblemVoteService : ICodeProblemVoteService
    {
        private readonly CodeProblemAssistantContext _db;
        private readonly IVoteMapper _mapper;

        public CodeProblemVoteService(CodeProblemAssistantContext db, IVoteMapper mapper)
        {
            _db = db.ThrowIfNull();
            _mapper = mapper.ThrowIfNull();
        }

        public async Task<List<Vote>> Query(CodeProblemVotesQueryRequest request, CancellationToken cancellationToken = default)
        {
            var query = _db.Votes.AsQueryable();

            if (request.Id.HasValue)
            {
                query = query.Where(v => v.Id == request.Id);
            }
            if (request.CodeProblemUUID.HasValue)
            {
                query = query.Where(v => v.CodeProblemUUID == request.CodeProblemUUID);
            }
            if (request.UserReferenceUUID.HasValue)
            {
                query = query.Where(v => v.UserReferenceUUID == request.UserReferenceUUID);
            }
            if (request.UpVote.HasValue)
            {
                query = query.Where(v => v.UpVote == request.UpVote);
            }

            var result = await query.ToListAsync(cancellationToken);

            return _mapper.Map(result);
        }

        public async Task Vote(CodeProblemVotePatchRequest request, CancellationToken cancellationToken = default)
        {
            var codeProblem = await _db.CodeProblems
                .Include(p => p.Votes)
                .FirstOrDefaultAsync(p => p.UUID == request.CodeProblemUUID, cancellationToken);
            if (codeProblem == null)
            {
                throw new ObjectNotFoundException(request.CodeProblemUUID, nameof(codeProblem));
            }

            var existingVote = codeProblem.Votes.FirstOrDefault(v => v.UserReferenceUUID == request.UserReferenceUUID);

            if (existingVote == null)
            {
                codeProblem.Votes.Add(new DataModels.Vote
                {
                    CodeProblemId = codeProblem.Id,
                    CodeProblemUUID = codeProblem.UUID,
                    UpVote = request.UpVote,
                    UserReferenceUUID = request.UserReferenceUUID,
                });
            }
            else if (existingVote.UpVote == request.UpVote)
            {
                codeProblem.Votes.Remove(existingVote);
            }
            else if (existingVote.UpVote != request.UpVote)
            {
                existingVote.UpVote = request.UpVote;
            }

            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
