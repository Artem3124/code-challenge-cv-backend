using CodeProblemAssistant.Api.Mappers;

namespace CodeProblemAssistant.Api.Configurations
{
    public static class MapperConfigurations
    {
        public static IServiceCollection AddMappers(this IServiceCollection services) => services
            .AddScoped<ICodeProblemMapper, CodeProblemMapper>()
            .AddScoped<ITagMapper, TagMapper>()
            .AddScoped<IVoteMapper, VoteMapper>();
    }
}
