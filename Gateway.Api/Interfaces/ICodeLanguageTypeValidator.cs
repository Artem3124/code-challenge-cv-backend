using Shared.Core.Enums;

namespace Gateway.Api.Interfaces
{
    public interface ICodeLanguageTypeValidator
    {
        bool IsAvailable(CodeLanguage codeLanguage);

        void Validate(CodeLanguage codeLanguage);

        List<CodeLanguage> GetSupportedCodeLanguages();

    }
}