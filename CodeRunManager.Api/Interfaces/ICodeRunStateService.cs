using CodeRunManager.Contract.Models;
using Shared.Core.Enums;

namespace CodeRunManager.Api.Interfaces
{
    public interface ICodeRunStageService
    {
        Task<CodeRunStage> GetAsync(Guid codeRunUUID, CancellationToken cancellationToken = default);

        Task<CodeRunResult?> GetResultAsync(Guid codeRunUUID, CancellationToken cancellationToken = default);

        Task<int> CompleteAsync(Guid codeRunUUID, CodeRunCompleteRequest request, CancellationToken cancellationToken = default);
    }
}