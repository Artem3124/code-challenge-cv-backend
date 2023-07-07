using Microsoft.CodeAnalysis;

namespace SolutionValidator.Cs.Interfaces
{
    internal interface IExecutableReferenceService
    {
        PortableExecutableReference GetReferenceFromLocation(string location);

        IEnumerable<PortableExecutableReference> GetReferenceFromLocation(string[] locations);
    }
}