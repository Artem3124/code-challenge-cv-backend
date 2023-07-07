using CodeRunManager.Contract.Interfaces;
using CodeRunManager.Contract.Models;
using Shared.Core.Clients;
using Shared.Core.Enums;

namespace CodeRunManager.Contract.Clients
{
    internal class CodeRunManagerClient : HttpClientBase, ICodeRunManagerClient
    {
        public CodeRunManagerClient(string baseUrl) : base(baseUrl ?? "https://localhost:7179")
        {

        }

        public async Task<Guid> QueueRunAsync(CodeRunQueueRequest request, CancellationToken cancellationToken = default)
        {
            return await Post<Guid, CodeRunQueueRequest>("api/CodeRun", request, cancellationToken);
        }

        public Task<CodeRunResult?> GetRunResultAsync(Guid runUUID, CancellationToken cancellationToken = default)
        {
            return Get<CodeRunResult?>($"api/CodeRunResult/{runUUID}", cancellationToken);
        }

        public Task<CodeRunStage> GetStageAsync(Guid runUUID, CancellationToken cancellationToken = default)
        {
            return Get<CodeRunStage>($"api/CodeRunResult/{runUUID}/stage", cancellationToken);
        }

        public Task<List<CodeRunResult>> QueryCodeRunsAsync(CodeRunResultQueryRequest request, CancellationToken cancellationToken = default)
        {
            return Post<List<CodeRunResult>, CodeRunResultQueryRequest>("api/CodeRunResult", request, cancellationToken);
        }

        public Task<List<CodeRunResultExpanded>> QueryCodeRunsExpandedAsync(CodeRunResultQueryRequest request, CancellationToken cancellationToken = default)
        {
            return Post<List<CodeRunResultExpanded>, CodeRunResultQueryRequest>("api/CodeRunResult/expanded", request, cancellationToken);
        }

        public Task<UserStatistic> GetUserStatisticAsync(Guid userUUID, CancellationToken cancellationToken = default)
        {
            return Get<UserStatistic>($"api/Statistic/{userUUID}", cancellationToken);
        }

        public Task<int> UpdateCodeRunStageAsync(Guid codeRunUUID, CodeRunStageUpdateRequest request, CancellationToken cancellationToken = default)
        {
            return Put<int, CodeRunStageUpdateRequest>($"api/CodeRunStage/{codeRunUUID}", request, cancellationToken);
        }

        public Task<int> CompleteCodeRunAsync(Guid codeRunUUID, CodeRunCompleteRequest request, CancellationToken cancellationToken = default)
        {
            return Post<int, CodeRunCompleteRequest>($"api/CodeRunStage/{codeRunUUID}", request, cancellationToken);
        }
    }
}
