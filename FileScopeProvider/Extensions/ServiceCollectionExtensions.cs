using FileScopeProvider.Interfaces;
using FileScopeProvider.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FileScopeProvider.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseFileScopeProvider(this IServiceCollection services) => services
            .AddScoped<IFilePurger, FilePurger>()
            .AddScoped<IFileService, FileService>()
            .AddScoped<IFileScopeProvider, FileScopeProvider>();
    }
}
