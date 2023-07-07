using Microsoft.CodeAnalysis;
using Shared.Core.Extensions;

namespace SolutionValidator.Cs.Models
{
    public class CsCompilationContext
    {
        public List<SyntaxTree> SyntaxTrees { get; set; }

        public HashSet<string> ReferenceLocations { get; set; }

        public CsCompilationContext(List<SyntaxTree> syntaxTrees) : this(syntaxTrees, new())
        {

        }

        public CsCompilationContext(List<SyntaxTree> syntaxTrees, HashSet<string> referenceLocations)
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
