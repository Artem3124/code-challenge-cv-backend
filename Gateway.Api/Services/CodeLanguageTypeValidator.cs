using Gateway.Api.Interfaces;
using Shared.Core.Enums;

namespace Gateway.Api.Services
{
    public class CodeLanguageTypeValidator : ICodeLanguageTypeValidator
    {
        private readonly List<CodeLanguage> _supportedCodeLanguages;

        public CodeLanguageTypeValidator(List<CodeLanguage> codeLanguages)
        {
            _supportedCodeLanguages = codeLanguages ?? new List<CodeLanguage>();
        }

        public void Validate(CodeLanguage codeLanguage)
        {
            if (!IsAvailable(codeLanguage))
            {
                throw new NotSupportedException($"Code language {codeLanguage} is not supported.");
            }
        }

        public bool IsAvailable(CodeLanguage codeLanguage)
        {
            return _supportedCodeLanguages.Contains(codeLanguage);
        }

        public List<CodeLanguage> GetSupportedCodeLanguages() => _supportedCodeLanguages;
    }
}
