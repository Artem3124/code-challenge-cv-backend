using CppTestContextBuilder.Core.Models;
using CppTestContextBuilder.Interfaces;
using CppTestContextBuilder.Providers;
using CppTestContextBuilder.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Interfaces;
using Shared.Core.Services;

namespace CppTestContextBuilder.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureCppTestContextBuilder(this IServiceCollection services, IConfiguration configuration) => services
            .Configure<CppTestContextBuilderSettings>(s =>
            {
                s.TestLibraryEntryPointPath = "../../../../CppTestFramework/TestContext.h";
                s.TestLibraryObjectFilePath = "../../../../CppTestFramework/CppTestFramework.o";
                s.FileNamePrefix = "_solution";
                s.FileNamePostfix = string.Empty;
            })
            .AddSingleton<ICppIncludesProvider, CppIncludesProvider>()
            .AddScoped<IIncludeService, IncludeService>()
            .AddScoped<IFileNameGenerator, FileNameGenerator>()
            .AddScoped<ICppTestContextBuilder, CppTestContextBuilder>();
    }
}
