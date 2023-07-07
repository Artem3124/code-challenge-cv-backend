using Microsoft.CodeAnalysis;

namespace CSCompiler.Interfaces
{
    internal interface IReferencesProvider
    {
        IEnumerable<PortableExecutableReference> Get();
    }
}