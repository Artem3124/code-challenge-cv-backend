using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Compilers;
using Shared.Core.Extensions;

namespace SolutionValidator.Cpp.Wrappers
{
    internal class AssemblyWrapper : IAssemblyWrapper
    {
        private readonly IServiceProvider _serviceProvider;

        public AssemblyWrapper(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider.ThrowIfNull();
        }

        public IInternalAssembly Wrap(string executableName)
        {
            var scope = _serviceProvider.CreateScope();

            var assembly = scope.ServiceProvider.GetService<CppInternalAssembly>() ?? throw new ArgumentNullException(nameof(executableName));

            assembly.Load(executableName);

            return assembly;
        }
    }
}
