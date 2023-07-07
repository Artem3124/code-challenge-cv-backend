using CSCompiler.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace CSCompiler.Extensions
{
    public static class ConfigureService
    {
        public static IServiceCollection ConfigureCsCompiler(this IServiceCollection serviceCollection) =>
            DiConfig.Configure(serviceCollection);
    }
}
