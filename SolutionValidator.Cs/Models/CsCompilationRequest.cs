using Microsoft.CodeAnalysis;

namespace SolutionValidator.Cs.Models
{
    public class CSCompilationRequest
    {
        public string AssemblyName { get; set; } = string.Empty;

        public IEnumerable<string> References { get; set; } = new List<string>();

        public List<SyntaxTree> SyntaxTrees { get; set; }

        public CSCompilationRequest(CsCompilationContext compilationContext, string assemblyName)
            : this(compilationContext.SyntaxTrees, assemblyName, compilationContext.ReferenceLocations)
        {

        }

        public CSCompilationRequest(List<SyntaxTree> syntaxTrees, string assemblyName, IEnumerable<string> references)
        {
            SyntaxTrees = syntaxTrees;
            AssemblyName = assemblyName;
            References = references;
        }
    }
}
