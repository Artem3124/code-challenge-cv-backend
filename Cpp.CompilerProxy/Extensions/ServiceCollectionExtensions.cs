using Cpp.CompilerProxy.Core.Wrappers;
using Cpp.CompilerProxy.Services;
using Cpp.CompilerProxy.Wrappers;
using FileScopeProvider.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Cpp.CompilerProxy.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureCppCompilerAdapter(this IServiceCollection services) => services
            .AddScoped<CppInternalAssembly>()
            .AddScoped<ICompilationDiagnosticCheckService, CompilationDiagnosticCheckService>()
            .AddScoped<ICppCompilerAdapter, CppCompilerAdapter>()
            .AddScoped<IAssemblyWrapper, AssemblyWrapper>()
            .AddScoped<IDiagnosticParser, DiagnosticParser>()
            .UseFileScopeProvider();
    }
}
