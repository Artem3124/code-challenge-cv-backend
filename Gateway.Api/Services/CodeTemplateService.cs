using CodeProblemAssistant.Contract.Clients;
using Gateway.Api.Interfaces;
using Gateway.Contact.Models;
using Shared.Core.Enums;
using Shared.Core.Extensions;

namespace Gateway.Api.Services
{
    public class CodeTemplateService : ICodeTemplateService
    {
        private readonly ICodeProblemAssistantClient _codeProblemAssistantClient;
        private readonly ICodeLanguageTypeValidator _codeLanguageTypeValidator;

        public CodeTemplateService(ICodeProblemAssistantClient codeProblemAssistantClient, ICodeLanguageTypeValidator codeLanguageTypeValidator)
        {
            _codeProblemAssistantClient = codeProblemAssistantClient.ThrowIfNull();
            _codeLanguageTypeValidator = codeLanguageTypeValidator.ThrowIfNull();
        }

        public async Task<List<CodeProblemTemplate>> GetChallengeTemplatesAsync(Guid challengeUUID, CancellationToken cancellationToken = default)
        {
            var languages = _codeLanguageTypeValidator.GetSupportedCodeLanguages();

            var templates = new List<CodeProblemTemplate>();

            foreach (var l in languages)
            {
                templates.Add(new CodeProblemTemplate(
                    l,
                    await _codeProblemAssistantClient.GetChallengeTemplate(challengeUUID, l))
                );
            }

            return templates;
        }

        public async Task<CodeProblemTemplate> GetSolutionTemplateAsync(Guid codeProblemUID, CodeLanguage codeLanguage, CancellationToken cancellationToken = default)
        {
            _codeLanguageTypeValidator.Validate(codeLanguage);

            var template = await _codeProblemAssistantClient.GetSolutionTemplate(codeProblemUID, codeLanguage, cancellationToken);

            return new CodeProblemTemplate(codeLanguage, template);
        }

        public async Task<List<CodeProblemTemplate>> GetSolutionTemplatesAsync(Guid codeProblemUUID, CancellationToken cancellationToken = default)
        {
            var languages = _codeLanguageTypeValidator.GetSupportedCodeLanguages();

            var templates = new List<CodeProblemTemplate>();

            foreach (var l in languages)
            {
                templates.Add(new CodeProblemTemplate(
                    l,
                    await _codeProblemAssistantClient.GetSolutionTemplate(codeProblemUUID, l, cancellationToken))
                );
            }

            return templates;
        }
    }
}
