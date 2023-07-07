using Basic.Reference.Assemblies;
using CodeProblemAssistant.Contract;
using InternalTypes.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Compilers;
using Shared.Core.Interfaces;
using SolutionValidator.Cs.Clients;
using SolutionValidator.Cs.Interfaces;
using SolutionValidator.Cs.Mappers;
using SolutionValidator.Cs.Parsers;
using SolutionValidator.Cs.Providers;
using SolutionValidator.Cs.Services;
using SolutionValidator.Cs.Stages;
using SolutionValidator.Cs.Wrappers;
using SolutionValidators.Core.Interfaces;
using SolutionValidators.Core.Services;
using SolutionValidators.Core.Stages;
using TestCases.Extensions;

namespace SolutionValidator.Cs.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureCsSolutionValidator(this IServiceCollection services, IConfiguration configuration) => services
            .AddScoped<ICsContextBuilder, CsTestContextBuilder>()
            .AddScoped<IUsingService, UsingService>()
            .AddScoped<NUnitUsingsProvider>()
            .AddScoped<ICodeAutocompletionService, CsCodeAutocompletionService>()
            .UseTestCases(configuration)
            .UseInternalTypes()
            .AddScoped<XmlParser>()
            .AddScoped<ITestCaseMapper, TestCaseMapper>()
            .AddScoped<ITestCaseResultReader, CsTestCaseResultReader>()
            .AddSingleton(new NetReferencesProvider(ReferenceAssemblies.Net60))
            .AddScoped<IExecutableReferenceService, ExecutableReferenceService>()
            .AddScoped<ICsCompilerAdapter, CsCompilerAdapter>()
            .AddScoped<ICsCompilerClient, CsCompilerClient>()
            .AddScoped<ICompilationResultValidator, CompilationResultValidator>()
            .AddScoped<AssemblyWrapper>()
            .AddScoped<CsInternalAssembly>()
            .AddScoped<IDiagnosticMapper, DiagnosticMapper>()
            .AddScoped<InitialStage>()
            .AddScoped<CsCompilationStage>()
            .AddSingleton<IInternalAssemblyPool, InternalAssemblyPool>()
            .AddCodeProblemAssistantClient(configuration.GetValue<string>("Endpoints:CodeProblemAssistantClient"))
            .AddScoped<IStageCreator, StageCreator>()
            .AddScoped<ISystemProcessService, SystemProcessService>()
            .AddScoped<CsTestingStage>()
            .AddScoped<IStagesProvider, CsStagesProvider>()
            .AddScoped<CsStagesProvider>();
    }
}
