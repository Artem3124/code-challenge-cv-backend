using Basic.Reference.Assemblies;
using CSCompiler.Contract.Interfaces;
using CSCompiler.Core.Clients;
using CSCompiler.Core.Interfaces;
using CSCompiler.Core.Mappers;
using CSCompiler.Core.Providers;
using CSCompiler.Core.Services;
using CSCompiler.Core.Wrappers;
using Microsoft.Extensions.DependencyInjection;

namespace CSCompiler.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureCsCompiler(this IServiceCollection services) => services
            .AddSingleton(new NetReferencesProvider(ReferenceAssemblies.Net60))
            .AddScoped<IExecutableReferenceService, ExecutableReferenceService>()
            .AddScoped<ICSCompiler, Services.CSCompiler>()
            .AddScoped<ICSCompilerClient, CSCompilerClient>()
            .AddScoped<ICompilationResultValidator, CompilationResultValidator>()
            .AddScoped<AssemblyWrapper>()
            .AddScoped<CsInternalAssembly>()
            .AddScoped<IDiagnosticMapper, DiagnosticMapper>();
    }
}
