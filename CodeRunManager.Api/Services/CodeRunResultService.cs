using CodeProblemAssistant.Contract.Clients;
using CodeRunManager.Api.Interfaces;
using CodeRunManager.Api.Mappers;
using CodeRunManager.Contract.Models;
using CodeRunManager.Data;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Extensions;

namespace CodeRunManager.Api.Services
{
    public class CodeRunResultService : ICodeRunResultService
    {
        private readonly CodeRunManagerContext _db;
        private readonly ICodeRunResultMapper _mapper;

        public CodeRunResultService(CodeRunManagerContext db, ICodeRunResultMapper mapper)
        {
            _db = db.ThrowIfNull();
            _mapper = mapper.ThrowIfNull();
        }

        public async Task<List<CodeRunResultExpanded>> QueryExpandedAsync(CodeRunResultQueryRequest request, CancellationToken cancellationToken = default)
        {
            var resultExpanded = await Query(request)
                .Select(r => new
                {
                    r.RunTypeId,
                    r.CodeRun.CodeLanguageId,
                    r.CodeRun.SourceCode,
                    r.CodeRun.CodeProblemReferenceUUID,
                    CodeRunResult = r,
                })
                .ToListAsync(cancellationToken);

            return resultExpanded.Select(r => new CodeRunResultExpanded(
                _mapper.Map(r.CodeRunResult),
                r.CodeProblemReferenceUUID,
                r.SourceCode,
                r.CodeLanguageId,
                r.RunTypeId
            )).ToList();
        }

        public async Task<List<CodeRunResult>> QueryAsync(CodeRunResultQueryRequest request, CancellationToken cancellationToken = default)
        {
            var result = await Query(request).ToListAsync(cancellationToken);

            return _mapper.Map(result);
        }

        private IQueryable<Data.Models.CodeRunResult> Query(CodeRunResultQueryRequest request)
        {
            request.ThrowIfNull();

            var query = _db.CodeRunResults.AsQueryable();

            if (request.UUID.HasValue)
            {
                query = query.Where(r => r.UUID == request.UUID.Value);
            }

            if (request.CodeProblemUUID.HasValue)
            {
                query = query.Where(r => r.CodeRun.CodeProblemReferenceUUID == request.CodeProblemUUID.Value);
            }

            if (request.UserUUID.HasValue)
            {
                query = query.Where(r => r.CodeRun.UserReferenceUUID == request.UserUUID.Value);
            }

            if (request.RunType.HasValue)
            {
                query = query.Where(r => r.CodeRun.RunTypeId == request.RunType.Value);
            }

            if (request.Outcome.HasValue)
            {
                query = query.Where(r => r.CodeRunOutcomeId == request.Outcome.Value);
            }

            if (request.CodeRunUUID.HasValue)
            {
                query = query.Where(r => r.CodeRun.UUID == request.CodeRunUUID.Value);
            }

            return query;
        }
    }
}
