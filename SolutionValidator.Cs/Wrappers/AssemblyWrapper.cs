using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Compilers;
using Shared.Core.Extensions;
using System.Runtime.Loader;

namespace SolutionValidator.Cs.Wrappers;

#nullable disable
internal class AssemblyWrapper
{
    private readonly IServiceProvider _serviceProvider;

    public AssemblyWrapper(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public virtual IInternalAssembly Wrap(AssemblyLoadContext assemblyLoadContext, FileStream fileStream)
    {
        var scope = _serviceProvider.CreateScope();
        var internalAssembly = scope.ServiceProvider.GetService<CsInternalAssembly>().ThrowIfNull();

        internalAssembly.LoadAssembly(assemblyLoadContext, fileStream);

        return internalAssembly;
    }
#nullable disable
}
