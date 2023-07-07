using CodeProblemAssistant.Contract.Clients;
using Microsoft.Extensions.DependencyInjection;

namespace CodeProblemAssistant.Contract
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCodeProblemAssistantClient(this IServiceCollection services, string endpointUrl) =>
            services.AddScoped<ICodeProblemAssistantClient, CodeProblemAssistantClient>(cfg => new CodeProblemAssistantClient(endpointUrl));
    }
}
