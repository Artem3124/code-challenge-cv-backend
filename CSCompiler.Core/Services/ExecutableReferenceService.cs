using CSCompiler.Core.Interfaces;
using Microsoft.CodeAnalysis;
using Shared.Core.Extensions;

namespace CSCompiler.Core.Services
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
            locations.ThrowIfNull();

            return locations.Select(l => MetadataReference.CreateFromFile(l));
        }
    }
}
