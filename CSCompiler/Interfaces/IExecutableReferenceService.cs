using Microsoft.CodeAnalysis;

namespace CSCompiler.Interfaces
{
    internal interface IExecutableReferenceService
    {
        PortableExecutableReference GetReferenceFromLocation(string location);

        IEnumerable<PortableExecutableReference> GetReferenceFromLocation(string[] locations);
    }
}