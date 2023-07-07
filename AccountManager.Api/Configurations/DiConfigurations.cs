using AccountManager.Api.Interfaces;
using AccountManager.Api.Services;

namespace AccountManager.Api.Configurations
{
    internal static class DiConfigurations
    {
        public static IServiceCollection Configure(IServiceCollection services) => services
            .AddScoped<IAuthorizationService, AuthorizationService>()
            .AddScoped<IHashService, HashService>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<IUserValidator, UserValidator>()
            .AddScoped<ISubscriptionService, SubscriptionService>()
            .AddScoped<IUserUpdateValidator, UserUpdateValidator>()
            .AddLogging()
            .AddMappers();
    }
}
