using Cs.CodeAutocompletion.Interfaces;
using Cs.CodeAutocompletion.Services;
using Cs.TestContextBuilder.Interfaces;
using Cs.TestContextBuilder.Services;
using InternalTypes.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Cs.TestContextBuilder.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureCsTestContextBuilder(this IServiceCollection services) => services
            .AddScoped<ICSContextBuilder, CsTestContextBuilder>()
            .AddScoped<IUsingService, UsingService>()
            .AddScoped<NUnitUsingsProvider>()
            .AddScoped<ICodeAutocompletionService, CsCodeAutocompletionService>()
            .UseInternalTypes();
    }
}
