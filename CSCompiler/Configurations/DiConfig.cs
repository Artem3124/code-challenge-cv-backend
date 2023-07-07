using Basic.Reference.Assemblies;
using CSCompiler.Interfaces;
using CSCompiler.Providers;
using CSCompiler.Services;
using Microsoft.Extensions.DependencyInjection;
using NoName.Core.Interfaces;
using NoName.Core.Services;

namespace CSCompiler.Configurations
{
    public static class DiConfig
    {
        public static IServiceCollection Configure(IServiceCollection serviceCollection) => serviceCollection
            .AddSingleton<ILogger, ConsoleLogger>()
            .AddSingleton(new NetReferencesProvider(ReferenceAssemblies.Net60))
            .AddScoped<IExecutableReferenceService, ExecutableReferenceService>()
            .AddScoped<ICSCompiler, CSCompiler.Services.CSCompiler>();
    }
}
