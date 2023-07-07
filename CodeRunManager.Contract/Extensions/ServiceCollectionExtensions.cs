using CodeRunManager.Contract.Clients;
using CodeRunManager.Contract.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CodeRunManager.Contract.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCodeRunManagerClient(this IServiceCollection services, string endpointUrl) =>
            services.AddScoped<ICodeRunManagerClient, CodeRunManagerClient>(cfg => new CodeRunManagerClient(endpointUrl));
    }
}
