using CodeProblemAssistant.Api.Interfaces;
using CodeProblemAssistant.Api.Mappers;
using CodeProblemAssistant.Api.Providers;
using CodeProblemAssistant.Api.Services;
using Cs.CodeAutocompletion.Providers;
using InternalTypes.Extensions;
using SolutionValidator.Cpp.Extensions;
using SolutionValidator.Cs.Extensions;
using SolutionValidator.Python.Extensions;
using TestCases.Extensions;

namespace CodeProblemAssistant.Api.Configurations
{
    public static class DiConfigurations
    {
        public static IServiceCollection Configure(IServiceCollection services, IConfiguration configuration) => services
            .AddScoped<ICodeProblemService, CodeProblemService>()
            .AddScoped<ICodeTemplateProviderFactory, CodeTemplateProviderFactory>()
            .AddScoped<CsCodeTemplateProvider>()
            .AddScoped<ICodeTemplateService, CodeTemplateService>()
            .AddScoped<IChallengeAttemptService, ChallengeAttemptService>()
            .ConfigureCsSolutionValidator(configuration)
            .ConfigureCppSolutionValidator(configuration)
            .ConfigurePythonSolutionValidator(configuration)
            .AddScoped<PythonCodeTemplateProvider>()
            .AddScoped<CodeProblemMethodInfoMapper>()
            .AddScoped<CppCodeTemplateProvider>()
            .AddScoped<ICodeProblemVoteService, CodeProblemVoteService>()
            .AddScoped<ITagService, TagService>()
            .UseTestCases(configuration)
            .UseInternalTypes()
            .AddMappers()
            .AddLogging();
    }
}
