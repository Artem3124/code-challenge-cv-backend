using Microsoft.CodeAnalysis;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace SolutionValidator.Cs.Providers
{
    internal class NetReferencesProvider
    {
        public IEnumerable<PortableExecutableReference> References { init; get; }

        public NetReferencesProvider(IEnumerable<PortableExecutableReference> references)
        {
            References = references ?? new List<PortableExecutableReference>();
        }
    }
}
