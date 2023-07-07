namespace Shared.Core.Interfaces
{
    public interface ICodeTemplateProvider
    {
        Task<string> GetSolutionTemplate(Guid codeProblemUUID, CancellationToken cancellationToken = default);

        Task<string> GetChallengeTemplate(Guid challengeUUID, CancellationToken cancellationToken = default);
    }
}