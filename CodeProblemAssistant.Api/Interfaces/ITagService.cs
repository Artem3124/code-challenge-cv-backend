using CodeProblemAssistant.Contract.Models;

namespace CodeProblemAssistant.Api.Interfaces
{
    public interface ITagService
    {
        Task<List<Tag>> Query(TagQueryRequest request, CancellationToken cancellationToken = default);
        Task<Tag> GetOrCreateAsync(string name, int codeProblemId, CancellationToken cancellationToken = default);
        Task<List<Tag>> GetOrCreateBatchAsync(List<string> names, Guid codeProblemUUID, CancellationToken cancellationToken = default);
    }
}