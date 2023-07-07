using Gateway.Api.Mappers;

namespace Gateway.Api.Configurations
{
    public static class MapperConfigurations
    {
        public static IServiceCollection AddMappers(this IServiceCollection services) => services
            .AddScoped<ICodeProblemMapper, CodeProblemMapper>();
    }
}
