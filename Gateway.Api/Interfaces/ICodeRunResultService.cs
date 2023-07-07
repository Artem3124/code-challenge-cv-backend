using CodeRunManager.Contract.Models;

namespace Gateway.Api.Interfaces
{
    public interface ICodeRunResultService
    {
        Task<CodeRunResultExpanded> GetByUUIDAsync(Guid userUUID, Guid codeRunResultUUID, CancellationToken cancellationToken = default);

        Task<List<CodeRunResultExpanded>> GetByCodeProblemUUIDAsync(Guid userUUID, Guid codeProblemUUID, CancellationToken cancellationToken = default);

        Task<List<CodeRunResultExpanded>> GetByUserUUIDAsync(Guid userUUID, CancellationToken cancellationToken = default);

        Task<CodeRunResultExpanded> GetByCodeCodeRunUUIDAsync(Guid userUUID, Guid codeRunUUID, CancellationToken cancellationToken = default);
    }
}