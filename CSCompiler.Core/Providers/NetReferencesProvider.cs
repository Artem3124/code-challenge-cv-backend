using Microsoft.CodeAnalysis;

namespace CSCompiler.Core.Providers
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
