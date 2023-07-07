using CodeRunManager.Api.Interfaces;
using CodeRunManager.Data;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Enums;
using Shared.Core.Exceptions;
using Shared.Core.Extensions;

namespace CodeRunManager.Api.Services
{
    internal class StagePatchService : IStagePatchService
    {
        private readonly CodeRunManagerContext _db;

        public StagePatchService(CodeRunManagerContext db)
        {
            _db = db.ThrowIfNull();
        }

        public async Task<int> PatchAsync(Guid codeRunUUID, CodeRunStage stage, CancellationToken cancellationToken = default)
        {
            var codeRun = await _db.CodeRuns.FirstOrDefaultAsync(r => r.UUID == codeRunUUID, cancellationToken);

            if (codeRun == null)
            {
                throw new ObjectNotFoundException(codeRunUUID, nameof(codeRun));
            }
            if (codeRun.Stage != stage)
            {
                codeRun.Stage = stage;

                return await _db.SaveChangesAsync(cancellationToken);
            }

            return 0;
        }
    }
}
