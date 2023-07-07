using CodeProblemAssistant.Contract.Models;

namespace CodeProblemAssistant.Api.Interfaces
{
    public interface ICodeProblemVoteService
    {
        Task<List<Vote>> Query(CodeProblemVotesQueryRequest request, CancellationToken cancellationToken = default);
        Task Vote(CodeProblemVotePatchRequest request, CancellationToken cancellationToken = default);
    }
}