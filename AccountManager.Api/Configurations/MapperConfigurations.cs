using AccountManager.Api.Mappers;

namespace AccountManager.Api.Configurations
{
    internal static class MapperConfigurations
    {
        public static IServiceCollection AddMappers(this IServiceCollection services) => services
            .AddScoped<IUserMapper, UserMapper>();
    }
}
