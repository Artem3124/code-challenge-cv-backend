using CodeProblemAssistant.Api.Interfaces;
using CodeProblemAssistant.Api.Providers;
using Cs.CodeAutocompletion.Providers;
using Shared.Core.Enums;
using Shared.Core.Extensions;
using Shared.Core.Interfaces;

namespace CodeProblemAssistant.Api.Services
{
    public class CodeTemplateProviderFactory : ICodeTemplateProviderFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public CodeTemplateProviderFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider.ThrowIfNull();
        }

        public ICodeTemplateProvider GetCodeTemplateProvider(CodeLanguage codeLanguage) => codeLanguage switch
        {
            CodeLanguage.Cs => _serviceProvider.GetRequiredService<CsCodeTemplateProvider>(),
            CodeLanguage.Cpp => _serviceProvider.GetRequiredService<CppCodeTemplateProvider>(),
            CodeLanguage.Python => _serviceProvider.GetRequiredService<PythonCodeTemplateProvider>(),
            _ => throw new NotSupportedException(),
        };
    }
}
