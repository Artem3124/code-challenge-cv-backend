using FileScopeProvider.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestCases.Providers;
using TestCases.Settings;

namespace TestCases.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseTestCases(this IServiceCollection services, IConfiguration configuration) => services
            .AddScoped<ITestCasesProvider, TestCasesProviderJson>()
            .Configure<TestCaseProviderSettings>(configuration.GetSection("TestCaseProvider"))
            .UseFileScopeProvider();
    }
}
