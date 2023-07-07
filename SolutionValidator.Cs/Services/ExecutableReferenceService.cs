using Microsoft.CodeAnalysis;
using Shared.Core.Extensions;
using SolutionValidator.Cs.Interfaces;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace SolutionValidator.Cs.Services
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
