using CodeRunManager.Contract.Models;
using Shared.Core.Enums;

namespace CodeRunManager.Contract.Interfaces
{
    public interface ICodeRunManagerClient
    {
        Task<Guid> QueueRunAsync(CodeRunQueueRequest request, CancellationToken cancellationToken = default);

        Task<CodeRunStage> GetStageAsync(Guid runUUID, CancellationToken cancellationToken = default);

        Task<CodeRunResult?> GetRunResultAsync(Guid runUUID, CancellationToken cancellationToken = default);

        Task<List<CodeRunResult>> QueryCodeRunsAsync(CodeRunResultQueryRequest request, CancellationToken cancellationToken = default);

        Task<List<CodeRunResultExpanded>> QueryCodeRunsExpandedAsync(CodeRunResultQueryRequest request, CancellationToken cancellationToken = default);

        Task<UserStatistic> GetUserStatisticAsync(Guid userUUID, CancellationToken cancellationToken = default);

        Task<int> UpdateCodeRunStageAsync(Guid codeRunUUID, CodeRunStageUpdateRequest request, CancellationToken cancellationToken = default);

        Task<int> CompleteCodeRunAsync(Guid codeRunUUID, CodeRunCompleteRequest request, CancellationToken cancellationToken = default);
    }
}
