using Shared.Core.Enums;
using Shared.Core.Interfaces;

namespace CodeProblemAssistant.Api.Interfaces
{
    public interface ICodeTemplateProviderFactory
    {
        ICodeTemplateProvider GetCodeTemplateProvider(CodeLanguage codeLanguage);
    }
}
