using AccountManager.Contract.Extensions;
using CodeProblemAssistant.Contract;
using CodeRunManager.Api.Services;
using CodeRunManager.Contract.Extensions;
using FileScopeProvider.Extensions;
using InternalTypes.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mongo.Data.Extensions;
using Shared.Core.Compilers;
using Shared.Core.Interfaces;
using SolutionValidator.Cpp.Extensions;
using SolutionValidator.Cs.Extensions;
using SolutionValidator.Python.Extensions;
using SolutionValidators.Core.Services;
using WorkerService.SolutionValidator.HostedServices;
using WorkerService.Stager.Interfaces;
using WorkerService.Stager.Services;

namespace WorkerService.Stager.Configurations
{
    internal static class DiConfigurations
    {
        public static IServiceCollection Configure(IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddScoped<QueueChecker>()
                .AddScoped<IStageService, StageService>()
                .AddSingleton<IInternalAssemblyPool, InternalAssemblyPool>()
                .AddSingleton<IStagePipelineBuilder, StagePipelineBuilder>()
                .AddScoped<IStageProviderCreator, StageProviderCreator>()
                .AddLogging(cfg =>
                {
                    cfg.ClearProviders();
                    cfg.AddConsole();
                })
                .UseFileScopeProvider();

            services
                .ConfigureCppSolutionValidator(configuration)
                .ConfigureCsSolutionValidator(configuration)
                .ConfigurePythonSolutionValidator(configuration);

            services
                .AddCodeProblemAssistantClient(configuration.GetValue<string>("Endpoints:CodeProblemAssistantClient"))
                .AddCodeRunManagerClient(configuration.GetValue<string>("Endpoints:CodeRunManagerClient"))
                .AddAccountManager(configuration.GetValue<string>("Endpoints:AccountManagerClient"))
                .ConfigureMongo(configuration)
                .UseInternalTypes();

            return services;
        }
    }
}
