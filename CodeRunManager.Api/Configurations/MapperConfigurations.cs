using CodeRunManager.Api.Mappers;

namespace CodeRunManager.Api.Configurations
{
    public static class MapperConfigurations
    {
        public static IServiceCollection AddMappers(this IServiceCollection services) => services
            .AddScoped<ICodeRunMapper, CodeRunMapper>()
            .AddScoped<ICodeRunResultMapper, CodeRunResultMapper>();
    }
}
