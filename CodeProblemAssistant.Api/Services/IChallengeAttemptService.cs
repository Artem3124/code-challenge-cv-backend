using CodeProblemAssistant.Contract.Models;

namespace CodeProblemAssistant.Api.Services
{
    public interface IChallengeAttemptService
    {
        Task<Guid> CreateAsync(ChallengeCreateRequest request, CancellationToken cancellationToken = default);
        Task<List<Challenge>> GetAsync(Guid userUUID, CancellationToken cancellationToken = default);
        Task<Challenge> GetAsync(Guid challengeUUID, Guid userUUID, CancellationToken cancellationToken = default);
        Task<ChallengeAttempt> StartAsync(ChallengeStartRequest request, CancellationToken cancellationToken = default);
        Task SubmitAsync(ChallengeSubmitRequest request, CancellationToken cancellationToken = default);
        Task<ChallengeAttempt> GetAttemptAsync(Guid uuid, CancellationToken cancellationToken = default);
        Task<bool> Patch(Guid uuid, ChallengeUpdateRequest request, CancellationToken cancellationToken = default);
    }
}