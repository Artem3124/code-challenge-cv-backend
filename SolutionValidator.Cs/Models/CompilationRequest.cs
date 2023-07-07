namespace SolutionValidator.Cs.Models
{
    internal class CompilationRequest
    {
        public CompilationContext Compilation { get; set; }

        public string AssemblyName { get; set; }

        public CompilationRequest(CompilationContext compilation, string assemblyName)
        {
            Compilation = compilation;
            AssemblyName = assemblyName;
        }

        public CompilationRequest(CSCompilationRequest request)
            : this(new CompilationContext(request.SyntaxTrees, request.References.ToHashSet()), request.AssemblyName) { }
    }
}
