﻿using Microsoft.CodeAnalysis;

namespace SolutionValidator.Cs.Models
{
    internal class CompilationContext
    {
        public List<SyntaxTree> SyntaxTrees { get; set; }

        public HashSet<string> ReferenceLocations { get; set; }

        public CompilationContext(List<SyntaxTree> syntaxTrees, HashSet<string> referenceLocations)
        {
            SyntaxTrees = syntaxTrees;
            ReferenceLocations = referenceLocations ?? new HashSet<string>();
        }
    }
}
