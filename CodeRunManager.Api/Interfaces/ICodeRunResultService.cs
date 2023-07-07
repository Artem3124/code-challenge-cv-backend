using CodeRunManager.Contract.Models;

namespace CodeRunManager.Api.Interfaces
{
    public interface ICodeRunResultService
    {
        Task<List<CodeRunResult>> QueryAsync(CodeRunResultQueryRequest request, CancellationToken cancellationToken = default);

        Task<List<CodeRunResultExpanded>> QueryExpandedAsync(CodeRunResultQueryRequest request, CancellationToken cancellationToken = default);
    }
}