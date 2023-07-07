using CodeProblemAssistant.Contract.Models;
using Shared.Core.Enums;
using TestCases.Models;

namespace CodeProblemAssistant.Contract.Clients
{
    public interface ICodeProblemAssistantClient
    {
        Task<CodeProblem> GetCodeProblemByUUIDAsync(Guid uuid, CancellationToken cancellationToken = default);

        Task<List<TestCase>> GetRunTestCasesByCodeProblemUUIDAsync(Guid uuid, CancellationToken cancellationToken = default);

        Task<List<TestCase>> GetSubmitTestCasesByCodeProblemUUIDAsync(Guid uuid, CancellationToken cancellationToken = default);

        Task<List<CodeProblem>> GetCodeProblemsAsync(CancellationToken cancellationToken = default);

        Task<Guid> CreateCodeProblemAsync(CodeProblemCreateRequest request, CancellationToken cancellationToken = default);

        Task<string> GetSolutionTemplate(Guid codeProblemUUID, CodeLanguage codeLanguage, CancellationToken cancellationToken = default);

        Task CodeProblemVotePatch(CodeProblemVotePatchRequest request, CancellationToken cancellationToken = default);

        Task<List<Vote>> QueryCodeProblemVotes(CodeProblemVotesQueryRequest request, CancellationToken cancellationToken = default);

        Task<int> CreateCodeProblemTag(TagCreateRequest request);

        Task<List<Tag>> QueryCodeProblemTags(TagQueryRequest request);

        Task CreateCodeProblemTagBatch(TagCreateRequestBatch request);

        Task<Guid> CreateChallengeAsync(ChallengeCreateRequest request, CancellationToken cancellationToken = default);

        Task<Challenge> GetChallengeAsync(Guid challengeUUID, Guid userUUId, CancellationToken cancellationToken = default);

        Task<List<Challenge>> GetAllChallengesAsync(Guid userUUID, CancellationToken cancellationToken = default);

        Task<ChallengeAttempt> StartAttemptAsync(ChallengeStartRequest request, CancellationToken cancellationToken = default);

        Task SubmitAttemptAsync(ChallengeSubmitRequest request, CancellationToken cancellationToken = default);

        Task<string> GetChallengeTemplate(Guid challengeUUID, CodeLanguage codeLanguage);

        Task<ChallengeAttempt> GetChallengeAttempt(Guid uuid);

        Task<bool> PatchChallengeAttempt(Guid uuid, ChallengeUpdateRequest request);
    }
}
