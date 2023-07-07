namespace Shared.Core.Compilers;

public class CompilationResult
{
    public IInternalAssembly? Assembly { get; private set; }

    public List<CompilationDiagnostic> Diagnostics { get; init; }

    public CompilationResult(IInternalAssembly? assembly)
    {
        Assembly = assembly;
        Diagnostics = new();
    }

    public CompilationResult(IInternalAssembly? assembly, List<CompilationDiagnostic> diagnostics)
    {
        Assembly = assembly;
        Diagnostics = diagnostics;
    }

    public CompilationResult(List<CompilationDiagnostic> diagnostics)
    {
        Diagnostics = diagnostics;
    }
}
