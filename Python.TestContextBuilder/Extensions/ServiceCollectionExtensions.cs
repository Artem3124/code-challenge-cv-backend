using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Python.CodeAutocomplition;
using Python.TestContextBuilder.Interfaces;
using Python.TestContextBuilder.Services;
using Shared.Core.Interfaces;

namespace Python.TestContextBuilder.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigurePythonTestContextBuilder(this IServiceCollection services, IConfiguration configuration) => services
            .AddScoped<PythonCodeAutocomplitionService>()
            .AddScoped<IPythonTestContextBuilder, PythonTestContextBuilder>()
            .AddScoped<IFileNameGenerator, StringFileNameGenerator>()
            .AddScoped<Random>();

    }
}
