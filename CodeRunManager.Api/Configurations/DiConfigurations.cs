using AccountManager.Contract.Extensions;
using CodeProblemAssistant.Contract;
using CodeRunManager.Api.Interfaces;
using CodeRunManager.Api.Services;
using InternalTypes.Extensions;
using Mongo.Data.Extensions;
using Shared.Core.Compilers;
using Shared.Core.Interfaces;
using Shared.Core.Services;


namespace CodeRunManager.Api.Configurations
{
    public static class DiConfigurations
    {
        public static IServiceCollection Configure(IServiceCollection services, IConfiguration configuration) => services
            .AddMappers()
            .AddLogging()
            .AddTransient<IQueueService, QueueService>()
            .AddScoped<ICodeRunStageService, CodeRunStageService>()
            .AddScoped<ICodeRunResultService, CodeRunResultService>()
            .AddAccountManager(configuration.GetValue<string>("Endpoints:AccountManagerClient"))
            .AddCodeProblemAssistantClient(configuration.GetValue<string>("Endpoints:CodeProblemAssistantClient"))
            .AddSingleton<IInternalAssemblyPool, InternalAssemblyPool>()
            .AddScoped<IStagePatchService, StagePatchService>()
            .AddScoped<IFileNameGenerator, FileNameGenerator>()
            .AddScoped<IStatisticService, StatisticService>()
            .ConfigureMongo(configuration);
    }
}
