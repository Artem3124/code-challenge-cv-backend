using Shared.Core.Enums;

namespace CodeProblemAssistant.Api.Interfaces
{
    public interface ICodeTemplateService
    {
        Task<string> GetSolutionTemplate(Guid codeProblemUUID, CodeLanguage codeLanguage, CancellationToken cancellationToken = default);

        Task<string> GetChallegenTemplate(Guid challengeUUID, CodeLanguage codeLanguage, CancellationToken cancellationToken = default);

    }
}