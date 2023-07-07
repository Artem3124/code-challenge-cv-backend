using CodeProblemAssistant.Api.Interfaces;
using Shared.Core.Enums;
using Shared.Core.Extensions;

namespace CodeProblemAssistant.Api.Services
{
    public class CodeTemplateService : ICodeTemplateService
    {
        private readonly ICodeTemplateProviderFactory _codeTemplateProviderFactory;

        public CodeTemplateService(ICodeTemplateProviderFactory codeTemplateProviderFactory)
        {
            _codeTemplateProviderFactory = codeTemplateProviderFactory.ThrowIfNull();
        }

        public Task<string> GetChallegenTemplate(Guid challengeUUID, CodeLanguage codeLanguage, CancellationToken cancellationToken = default)
        {
            var templateProvider = _codeTemplateProviderFactory.GetCodeTemplateProvider(codeLanguage);

            return templateProvider.GetChallengeTemplate(challengeUUID, cancellationToken);
        }

        public Task<string> GetSolutionTemplate(Guid codeProblemUUID, CodeLanguage codeLanguage, CancellationToken cancellationToken = default)
        {
            var templateProvider = _codeTemplateProviderFactory.GetCodeTemplateProvider(codeLanguage);

            return templateProvider.GetSolutionTemplate(codeProblemUUID, cancellationToken);
        }
    }
}
