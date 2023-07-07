using Mongo.Data.Models;
using Shared.Core.Enums;

namespace Mongo.Data.Services
{
    public interface ICodeRunQueueMessageService
    {
        Task<CodeRunQueueMessage> DequeueAsync(CancellationToken cancellationToken = default);
        Task EnqueueAsync(CodeRunQueueMessage message, PriorityType priority, CancellationToken cancellationToken = default);
    }
}