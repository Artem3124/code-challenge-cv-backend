using CSCompiler.Interfaces;
using Microsoft.CodeAnalysis;
using NoName.Core.Extensions;

namespace CSCompiler.Services
{
    internal class ExecutableReferenceService : IExecutableReferenceService
    {
        public PortableExecutableReference GetReferenceFromLocation(string location)
        {
            if (string.IsNullOrEmpty(location))
            {
                throw new ArgumentNullException(nameof(location));
            }

            return MetadataReference.CreateFromFile(location);
        }

        public IEnumerable<PortableExecutableReference> GetReferenceFromLocation(string[] locations)
        {
            locations.ThrowIfNull(nameof(locations));

            return locations.Select(l => MetadataReference.CreateFromFile(l));
        }
    }
}
