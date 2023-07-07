namespace CSCompiler.Models
{
    internal class CompilationContext
    {
        public string SourceCode { get; set; }

        public HashSet<string> ReferenceLocations { get; set; }

        public CompilationContext()
        {
            SourceCode = string.Empty;
            ReferenceLocations = new HashSet<string>();
        }

        public CompilationContext(string sourceCode, HashSet<string> referenceLocations)
        {
            SourceCode = sourceCode;
            ReferenceLocations = referenceLocations ?? new HashSet<string>();
        }
    }
}
