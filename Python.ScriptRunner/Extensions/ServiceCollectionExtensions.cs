using Microsoft.Extensions.DependencyInjection;

namespace Python.ScriptRunner.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigurePythonScriptRunner(this IServiceCollection services) => services
            .AddScoped<IPythonScriptRunner, PythonScriptRunner>();
    }
}
