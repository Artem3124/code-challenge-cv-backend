using CodeRunManager.Contract.Models;

namespace CodeRunManager.Api.Interfaces
{
    public interface IQueueService
    {
        Task<Guid> EnqueueAsync(CodeRunQueueRequest request, CancellationToken cancellationToken = default);
    }
}