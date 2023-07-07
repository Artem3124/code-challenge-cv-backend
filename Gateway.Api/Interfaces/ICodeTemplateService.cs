using Gateway.Contact.Models;
using Shared.Core.Enums;

namespace Gateway.Api.Interfaces
{
    public interface ICodeTemplateService
    {
        Task<CodeProblemTemplate> GetSolutionTemplateAsync(Guid codeProblemUID, CodeLanguage codeLanguage, CancellationToken cancellationToken = default);

        Task<List<CodeProblemTemplate>> GetSolutionTemplatesAsync(Guid codeProblemUUID, CancellationToken cancellationToken = default);

        Task<List<CodeProblemTemplate>> GetChallengeTemplatesAsync(Guid challengeUUID, CancellationToken cancellationToken = default);
    }
}