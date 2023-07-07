using CodeProblemAssistant.Contract;
using InternalTypes.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Interfaces;
using SolutionValidator.Python.Interfaces;
using SolutionValidator.Python.Providers;
using SolutionValidator.Python.Services;
using SolutionValidator.Python.Stages;
using SolutionValidators.Core.Stages;

namespace SolutionValidator.Python.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigurePythonSolutionValidator(this IServiceCollection services, IConfiguration configuration) => services
            .AddScoped<IPythonScriptRunner, PythonScriptRunner>()
            .AddScoped<PythonCodeAutocomplitionService>()
            .AddScoped<PythonSyntaxCheckResultParser>()
            .UseInternalTypes()
            .AddScoped<IPythonTestContextBuilder, PythonTestContextBuilder>()
            .AddScoped<IFileNameGenerator, StringFileNameGenerator>()
            .AddScoped<PythonTestCaseResultParser>()
            .AddScoped<InitialStage>()
            .AddScoped<PythonSyntaxCheckStage>()
            .AddScoped<PythonTestingStage>()
            .AddScoped<PythonStagesProvider>()
            .AddCodeProblemAssistantClient(configuration.GetValue<string>("Endpoints:CodeProblemAssistantClient"))
            .AddScoped<Random>();
    }
}
