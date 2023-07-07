using FileScopeProvider.Interfaces;
using FileScopeProvider.Models;
using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Extensions;

namespace FileScopeProvider
{
    internal class FileScopeProvider : IFileScopeProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public FileScopeProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider.ThrowIfNull();
        }

        public IFileScope Create(List<SystemFile> files)
        {
            var scope = _serviceProvider.CreateScope();

            return new FileScope(GetServiceOrThrow<IFileService>(scope), GetServiceOrThrow<IFilePurger>(scope), files);
        }

        private T GetServiceOrThrow<T>(IServiceScope scope)
        {
            var service = scope.ServiceProvider.GetService<T>();
            if (service == null)
            {
                throw new Exception("Unable to create service.");
            }

            return service;
        }
    }
}