using Shared.Core.Enums;

namespace CodeRunManager.Api.Interfaces
{
    public interface IStagePatchService
    {
        Task<int> PatchAsync(Guid codeRunUUID, CodeRunStage stage, CancellationToken cancellationToken = default);
    }
}