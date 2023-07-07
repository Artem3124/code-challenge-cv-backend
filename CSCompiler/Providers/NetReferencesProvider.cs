using Microsoft.CodeAnalysis;

namespace CSCompiler.Providers
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
