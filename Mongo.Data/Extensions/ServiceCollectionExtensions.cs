using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mongo.Data.Services;
using Mongo.Data.Settings;

namespace Mongo.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureMongo(this IServiceCollection services, IConfiguration configuration) =>
            services.Configure<CodeSkillsDatabaseSettings>(configuration.GetSection("CodeSkillsDatabase"))
            .AddScoped<ICodeRunQueueMessageService, CodeRunQueueMessageService>()
            .AddScoped<ITestCaseService, TestCaseService>();
    }
}
