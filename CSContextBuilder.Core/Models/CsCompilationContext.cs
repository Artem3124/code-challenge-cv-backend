using Microsoft.CodeAnalysis;
using Shared.Core.Extensions;

namespace CSTestContextBuilder.Core.Models
{
    public class CSCompilationContext
    {
        public List<SyntaxTree> SyntaxTrees { get; set; }

        public HashSet<string> ReferenceLocations { get; set; }

        public CSCompilationContext(List<SyntaxTree> syntaxTrees) : this(syntaxTrees, new())
        {
        }

        public CSCompilationContext(List<SyntaxTree> syntaxTrees, HashSet<string> referenceLocations)
        {
            syntaxTrees.ThrowIfNull();
            if (syntaxTrees == null || syntaxTrees.Count == 0)
            {
                throw new ArgumentException("No syntax threes were provided.");
            }
            SyntaxTrees = syntaxTrees;
            ReferenceLocations = referenceLocations;
        }
    }
}
