using FileScopeProvider.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Interfaces;
using Shared.Core.Services;
using SolutionValidator.Cpp.Interfaces;
using SolutionValidator.Cpp.Providers;
using SolutionValidator.Cpp.Services;
using SolutionValidator.Cpp.Wrappers;
using SolutionValidator.Cpp.Models;
using SolutionValidator.Cpp.Stages;
using Microsoft.Extensions.Configuration;
using InternalTypes.Extensions;
using SolutionValidators.Core.Interfaces;
using WorkerService.Stager.Services;
using Shared.Core.Compilers;
using SolutionValidators.Core.Stages;
using CodeProblemAssistant.Contract;
using SolutionValidators.Core.Services;

namespace SolutionValidator.Cpp.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureCppSolutionValidator(this IServiceCollection services, IConfiguration configuration) => services
            .AddScoped<CppInternalAssembly>()
            .AddScoped<ICompilationDiagnosticCheckService, CompilationDiagnosticCheckService>()
            .AddScoped<ICppCompilerAdapter, CppCompilerAdapter>()
            .AddScoped<IAssemblyWrapper, AssemblyWrapper>()
            .AddScoped<IDiagnosticParser, DiagnosticParser>()
            .UseFileScopeProvider()
            .Configure<CppTestContextBuilderSettings>(s =>
            {
                s.TestLibraryEntryPointPath = "../../../../CppTestFramework/TestContext.h";
                s.TestLibraryObjectFilePath = "../../../../CppTestFramework/CppTestFramework.o";
                s.FileNamePrefix = "_solution";
                s.FileNamePostfix = string.Empty;
            })
            .AddCodeProblemAssistantClient(configuration.GetValue<string>("Endpoints:CodeProblemAssistantClient"))
            .AddSingleton<ICppIncludesProvider, CppIncludesProvider>()
            .AddScoped<IIncludeService, IncludeService>()
            .AddScoped<IFileNameGenerator, FileNameGenerator>()
            .AddScoped<ICppTestContextBuilder, CppTestContextBuilder>()
            .AddScoped<CppStagesProvider>()
            .AddScoped<CppTestCaseResultReader>()
            .AddScoped<ITestCaseResultInputRestoreService, TestCaseResultInputRestoreService>()
            .AddScoped<CppCodeAutocomplitionService>()
            .AddScoped<InitialStage>()
            .AddScoped<ISystemProcessService, SystemProcessService>()
            .AddScoped<CppInternalAssembly>()
            .AddScoped<CppCompilationStage>()
            .AddSingleton<IInternalAssemblyPool, InternalAssemblyPool>()
            .AddScoped<CppTestingStage>(cfg =>
            {
                var assemblyPool = cfg.GetService<IInternalAssemblyPool>();
                var testCaseResultReader = cfg.GetService<CppTestCaseResultReader>();
                var resultRestoreService = cfg.GetService<ITestCaseResultInputRestoreService>();
                return new(assemblyPool, testCaseResultReader, resultRestoreService);
            })
            .UseInternalTypes();
    }
}
