using CodeProblemAssistant.Contract.Models;
using Shared.Core.Clients;
using Shared.Core.Enums;
using System.Threading;
using TestCases.Models;

namespace CodeProblemAssistant.Contract.Clients
{
    public class CodeProblemAssistantClient : HttpClientBase, ICodeProblemAssistantClient
    {
        // TODO: Create config for this
        public CodeProblemAssistantClient(string baseUrl) : base(baseUrl ?? "https://localhost:7077")
        {

        }

#nullable disable
        public Task<CodeProblem> GetCodeProblemByUUIDAsync(Guid uuid, CancellationToken cancellationToken = default)
        {
            return Get<CodeProblem>($"api/CodeProblem/{uuid}", cancellationToken);
        }

        public Task<Guid> CreateCodeProblemAsync(CodeProblemCreateRequest request, CancellationToken cancellationToken = default)
        {
            return Post<Guid, CodeProblemCreateRequest>("api/CodeProblem", request, cancellationToken);
        }

        public Task<List<CodeProblem>> GetCodeProblemsAsync(CancellationToken cancellationToken = default)
        {
            return Get<List<CodeProblem>>($"api/CodeProblem", cancellationToken);
        }

        public Task<List<TestCase>> GetRunTestCasesByCodeProblemUUIDAsync(Guid uuid, CancellationToken cancellationToken = default)
        {
            return Get<List<TestCase>>($"api/CodeProblem/{uuid}/runTestSet", cancellationToken);
        }

        public Task<List<TestCase>> GetSubmitTestCasesByCodeProblemUUIDAsync(Guid uuid, CancellationToken cancellationToken = default)
        {
            return Get<List<TestCase>>($"api/CodeProblem/{uuid}/submitTestSet", cancellationToken);
        }

        public Task<string> GetSolutionTemplate(Guid codeProblemUUID, CodeLanguage codeLanguage, CancellationToken cancellationToken = default)
        {
            return Get<string>($"api/CodeProblem/{codeProblemUUID}/template/{codeLanguage}", cancellationToken);
        }

        public Task CodeProblemVotePatch(CodeProblemVotePatchRequest request, CancellationToken cancellationToken = default)
        {
            return Post<bool, CodeProblemVotePatchRequest>("api/CodeProblemVote", request, cancellationToken);
        }

        public Task<List<Vote>> QueryCodeProblemVotes(CodeProblemVotesQueryRequest request, CancellationToken cancellationToken = default)
        {
            return Put<List<Vote>, CodeProblemVotesQueryRequest>("api/CodeProblemVote", request, cancellationToken);
        }

        public Task<int> CreateCodeProblemTag(TagCreateRequest request)
        {
            return Post<int, TagCreateRequest>("api/CodeProblemTag", request);
        }

        public Task<List<Tag>> QueryCodeProblemTags(TagQueryRequest request)
        {
            return Put<List<Tag>, TagQueryRequest>("api/CodeProblemTag", request);
        }

        public Task CreateCodeProblemTagBatch(TagCreateRequestBatch request)
        {
            return Post<int, TagCreateRequestBatch>("api/CodeProblemTag/batch", request);
        }

        public Task<Guid> CreateChallengeAsync(ChallengeCreateRequest request, CancellationToken cancellationToken = default)
        {
            return Post<Guid, ChallengeCreateRequest>("api/Challenge", request, cancellationToken);
        }

        public Task<Challenge> GetChallengeAsync(Guid challengeUUID, Guid userUUID, CancellationToken cancellationToken = default)
        {
            return Get<Challenge>($"api/Challenge/{userUUID}/{challengeUUID}", cancellationToken);
        }

        public Task<List<Challenge>> GetAllChallengesAsync(Guid userUUID, CancellationToken cancellationToken = default)
        {
            return Get<List<Challenge>>($"api/Challenge/{userUUID}", cancellationToken);
        }

        public Task<ChallengeAttempt> StartAttemptAsync(ChallengeStartRequest request, CancellationToken cancellationToken = default)
        {
            return Post<ChallengeAttempt, ChallengeStartRequest>("api/ChallengeAttempt", request, cancellationToken);
        }

        public Task SubmitAttemptAsync(ChallengeSubmitRequest request, CancellationToken cancellationToken = default)
        {
            return Put<bool, ChallengeSubmitRequest>("api/ChallengeAttempt", request, cancellationToken);
        }

        public Task<string> GetChallengeTemplate(Guid challengeUUID, CodeLanguage codeLanguage)
        {
            return Get<string>($"api/CodeProblem/{challengeUUID}/challengeTemplate/{codeLanguage}");
        }

        public Task<ChallengeAttempt> GetChallengeAttempt(Guid uuid)
        {
            return Get<ChallengeAttempt>($"api/ChallengeAttempt/{uuid}");
        }

        public Task<bool> PatchChallengeAttempt(Guid uuid, ChallengeUpdateRequest request)
        {
            return Put<bool, ChallengeUpdateRequest>($"api/ChallengeAttempt/patch/{uuid}", request);
        }
#nullable enable
    }
}
