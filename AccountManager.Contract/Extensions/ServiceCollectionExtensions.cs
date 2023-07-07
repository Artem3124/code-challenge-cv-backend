using Microsoft.Extensions.DependencyInjection;

namespace AccountManager.Contract.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAccountManager(this IServiceCollection services, string endpointUrl) =>
            services.AddScoped<IAccountManagerClient>(cfg => new AccountManagerClient(endpointUrl));
    }
}
