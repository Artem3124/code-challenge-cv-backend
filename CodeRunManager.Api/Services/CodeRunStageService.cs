using CodeRunManager.Api.Interfaces;
using CodeRunManager.Api.Mappers;
using CodeRunManager.Contract.Models;
using CodeRunManager.Data;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shared.Core.Enums;
using Shared.Core.Exceptions;
using Shared.Core.Extensions;

namespace CodeRunManager.Api.Services
{
    public class CodeRunStageService : ICodeRunStageService
    {
        private readonly CodeRunManagerContext _db;
        private readonly ICodeRunResultMapper _mapper;

        public CodeRunStageService(CodeRunManagerContext db, ICodeRunResultMapper mapper)
        {
            _db = db.ThrowIfNull();
            _mapper = mapper.ThrowIfNull();
        }

        public async Task<CodeRunStage> GetAsync(Guid codeRunUUID, CancellationToken cancellationToken = default)
        {
            var codeRun = await _db.CodeRuns.FirstOrDefaultAsync(r => r.UUID == codeRunUUID, cancellationToken);
            if (codeRun == default)
            {
                throw new ObjectNotFoundException(codeRunUUID, nameof(codeRun));
            }

            return codeRun.Stage;
        }

        public Task<int> CompleteAsync(Guid codeRunUUID, CodeRunCompleteRequest request, CancellationToken cancellationToken = default)
        {
            var codeRun = _db.CodeRuns.FirstOrDefault(r => r.UUID == codeRunUUID);
            if (codeRun == null)
            {
                throw new ObjectNotFoundException(codeRunUUID, nameof(codeRun));
            }

            codeRun.CodeRunResult = new Data.Models.CodeRunResult
            {
                UUID = Guid.NewGuid(),
                CodeRunOutcomeId = request.Outcome,
                Metadata = JsonConvert.SerializeObject(request.Metadata),
                CreatedAtUtc = DateTime.UtcNow,
                RunTypeId = codeRun.RunTypeId,
            };
            return _db.SaveChangesAsync(cancellationToken);
        }

        public async Task<CodeRunResult?> GetResultAsync(Guid codeRunUUID, CancellationToken cancellationToken = default)
        {
            var result = await _db.CodeRunResults.Where(r => r.CodeRun.UUID == codeRunUUID)
                .FirstOrDefaultAsync(cancellationToken);

            if (result == null)
            {
                return null;
            }

            return _mapper.Map(result);
        }
    }
}
