namespace CSCompiler.Models
{
    internal class CSCompilationRequest
    {
        public CompilationContext Compilation { get; set; }

        public string AssemblyName { get; set; }

        public CSCompilationRequest(CompilationContext compilation, string assemblyName)
        {
            Compilation = compilation;
            AssemblyName = assemblyName;
        }

        //public CSCompilationRequest(CompilationRequest request)
        //    : this(request.SourceCode, request.AssemblyName ?? string.Empty, null) { }
    }
}
