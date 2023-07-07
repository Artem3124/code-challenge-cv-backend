using CodeProblemAssistant.Contract;
using CodeRunManager.Contract.Extensions;
using FileScopeProvider.Interfaces;
using FileScopeProvider.Services;
using Gateway.Api.Interfaces;
using Gateway.Api.Mappers;
using Gateway.Api.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mongo.Data.Extensions;
using TestCases.Extensions;

namespace CodeProblemPatcher
{
    internal static class DiConfig
    {
        public static IServiceCollection Config(this IServiceCollection services, IConfiguration configuration) => services
            .AddScoped<IFileService, FileService>()
            .AddScoped<ICodeProblemMapper, CodeProblemMapper>()
            .AddScoped<CodeProblemLoader>()
            .AddScoped<ICodeProblemProvider, CodeProblemProvider>()
            .AddCodeRunManagerClient(configuration.GetValue<string>("Endpoints:CodeRunManagerClient"))
            .AddCodeProblemAssistantClient(configuration.GetValue<string>("Endpoints:CodeProblemAssistantClient"))
            .UseTestCases(configuration)
            .ConfigureMongo(configuration);
    }
}
